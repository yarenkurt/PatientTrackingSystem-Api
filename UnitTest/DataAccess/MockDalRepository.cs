using System;
using System.Linq;
using System.Linq.Expressions;
using Core.Results;
using Core.Signatures;
using DataAccess.Repositories;
using Moq;

namespace UnitTest.DataAccess
{
    public class MockDalRepository<TEntity> : Mock<IRepository<TEntity>>
        where TEntity : class, IBaseEntity, new()
    {
        public MockDalRepository<TEntity> GetAll(IQueryable<TEntity> result)
        {
            Setup(x => x.TableNoTracking).Returns(result);
            return this;
        }

        public MockDalRepository<TEntity> GetById(TEntity result)
        {
            Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(result);
            return this;
        }

        public MockDalRepository<TEntity> Get(TEntity result)
        {
            Setup(x => x.GetAsync(It.IsAny<Expression<Func<TEntity, bool>>>())).ReturnsAsync(result);
            return this;
        }

        public MockDalRepository<TEntity> Insert(DataResult<TEntity> result)
        {
            Setup(x => x.InsertAsync(It.IsAny<TEntity>())).ReturnsAsync(result);
            return this;
        }

        public MockDalRepository<TEntity> Update(Result result, TEntity entityResult)
        {
            Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(entityResult);
            Setup(x => x.UpdateAsync(It.IsAny<TEntity>())).ReturnsAsync(result);
            return this;
        }

        public MockDalRepository<TEntity> Delete(Result result, TEntity entityResult)
        {
            Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(entityResult);
            Setup(x => x.DeleteAsync(It.IsAny<TEntity>())).ReturnsAsync(result);
            return this;
        }
    }
}