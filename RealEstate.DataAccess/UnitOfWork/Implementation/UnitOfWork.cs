﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RealEstate.Data.Context;
using RealEstate.DataAccess.Repository.Implementation;
using RealEstate.DataAccess.Repository.Interface;
using RealEstate.DataAccess.UnitOfWork.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.DataAccess.UnitOfWork.Implementation
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly RealEstateDbContext _context;
        private IExecutionStrategy strategy;
        private IGenericRepository<T> _repository;
        private IDbContextTransaction Transaction;

        private readonly string savepoint = "dbcontext save point";

        public UnitOfWork(RealEstateDbContext dbContext)
        {
            _context = dbContext;
        }

        public IGenericRepository<T> Repository => _repository ??= new GenericRepository<T>(_context);

        private async Task RollBack()
        {
            await Transaction.RollbackAsync();
        }

        public async Task<bool> SaveAsync()
        {
            bool result = false;
            try
            {
                strategy = _context.Database.CreateExecutionStrategy();

                await strategy.Execute(async () =>
                {
                    Transaction = await _context.Database.BeginTransactionAsync();
                    await Transaction.CreateSavepointAsync(savepoint);
                    result = await _context.SaveChangesAsync() >= 0;

                    await Transaction.CommitAsync();
                });
            }
            catch (Exception e)
            {
                await RollBack();
                throw new Exception(e.Message.Equals("An error occurred while updating the entries. See the inner exception for details.") ? e.InnerException.Message : e.Message);
            }
            finally
            {
                await Transaction.DisposeAsync();
            }

            return result;
        }
    }
}
