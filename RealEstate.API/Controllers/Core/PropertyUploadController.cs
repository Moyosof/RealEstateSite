using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using RealEstate.API.Helpers;
using RealEstate.Application.Contracts.Core;
using RealEstate.Domain.ReadOnly;
using RealEstate.DTO.WriteOnly.CoreDTO;
using System.Net;

namespace RealEstate.API.Controllers.Core
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyUploadController : BaseController
    {
        private readonly IPropertyUpload _property;

        public PropertyUploadController( IPropertyUpload propertyUpload)
        {
            _property = propertyUpload;
        }


        /// <summary>
        /// This is to get all courses from the DB
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get_all_property")]
        [ProducesResponseType(typeof(JsonMessage<string>), 200)]

        public async Task<IActionResult> GetAllProperty(CancellationToken cancellationToken)
        {
            var result = await _property.GetAllProperty(cancellationToken);

            return Ok(new JsonMessage<PropertyUploadDto>
            {
                Results2 = result.ToArray(),
                status = true,
                success_message = "Successful",
                status_code = (int)HttpStatusCode.OK
            });
        }

        [HttpGet]
        [Route("get_property_by_id")]
        [ProducesResponseType(typeof(JsonMessage<string>), 200)]

        public async Task<IActionResult> GetPropertyId(Guid Id)
        {
            var result = await _property.GetProperty(Id);
            if (result != null)
            {
                return Ok(new JsonMessage<PropertyUploadDto>()
                {
                    result = new List<PropertyUploadDto>()
                    {
                        result
                    },
                    status = true,
                    success_message = "Property gotten by ID",
                    status_code = (int)HttpStatusCode.OK
                });
            }
            return Ok(new JsonMessage<PropertyUploadDto>()
            {
                result = new List<PropertyUploadDto>()
                {
                    result
                },
                status = false,
                error_message = "No property found by that particual Id",
                status_code = (int)HttpStatusCode.NotFound

            });
        }


        [HttpPost]
        [Route("create_new_property")]
        [ProducesResponseType(typeof(JsonMessage<string>), 200)]

        public async Task<IActionResult> UploadProperty([FromForm] PropertyUploadDTO propertyUpload)
        {

            var result = await _property.UploadProperty(propertyUpload);
            if (string.IsNullOrWhiteSpace(result))
            {

                return Ok(new JsonMessage<string>()
                {
                    status = true,
                    success_message = "Course created successfully",
                    status_code = (int)HttpStatusCode.OK

                });
            }
            return Ok(new JsonMessage<string>()
            {
                error_message = result,
                status = false,
                status_code = (int)HttpStatusCode.BadRequest

            });
        }

        [HttpPut]
        [Route("uppdate_property")]
        [ProducesResponseType(typeof(JsonMessage<string>), 200)]

        public async Task<IActionResult> UpdateProperty([FromForm]PropertyUploadDTO propertyUpload, Guid id)
        {
            var result = await _property.UpdateProperty(propertyUpload, id);

            if (result is null)
            {
                return Ok(new JsonMessage<string>()
                {
                    error_message = "Property not Found",
                    status = false,
                    status_code = (int)HttpStatusCode.NotFound
                });
            }
            return Ok(new JsonMessage<string>()
            {
                status = true,
                success_message = "Property Updated successfully",
                status_code = (int)HttpStatusCode.OK
            });
        }

        [HttpDelete]
        [Route("delete_property")]
        [ProducesResponseType(typeof(JsonMessage<string>), 200)]

        public async Task<IActionResult> DeleteProperty(Guid id)
        {
            var result = await _property.DestroyProperty(id);
            if (string.IsNullOrWhiteSpace(result))
            {
                return Ok(new JsonMessage<string>()
                {
                    status = true,
                    success_message = "Property Deleted  Successfully",
                    status_code = (int)HttpStatusCode.OK
                });
            }
            return Ok(new JsonMessage<string>()
            {
                error_message = result,
                status = false,
                status_code = (int)HttpStatusCode.NotFound
            });
        }
    }
}
