using Business.Repositories;
using Core.Results;
using Core.Signatures;
using Moq;

namespace UnitTest.Business.Mocks
{
    public class MockServiceRepository<TEntity> : Mock<IServiceRepository<TEntity>>
        where TEntity : class, IBaseEntity, new()
    {
        public MockServiceRepository<TEntity> Get(TEntity result)
        {
            Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(result);
            return this;
        }

        public MockServiceRepository<TEntity> Insert(DataResult<TEntity> result)
        {
            Setup(x => x.InsertAsync(It.IsAny<TEntity>())).ReturnsAsync(result);
            return this;
        }

        public MockServiceRepository<TEntity> Update(Result result, TEntity entityResult)
        {
            Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(entityResult);
            Setup(x => x.UpdateAsync(It.IsAny<TEntity>())).ReturnsAsync(result);
            return this;
        }

        public MockServiceRepository<TEntity> Delete(DataResult<int> result, TEntity entityResult)
        {
            Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(entityResult);
            Setup(x => x.DeleteAsync(It.IsAny<int>())).ReturnsAsync(result);
            return this;
        }
    }
}