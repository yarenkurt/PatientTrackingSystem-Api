using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
    public abstract class MethodInterception : MethodInterceptionBaseAttribute
    {
        public virtual void OnBefore(IInvocation invocation)
        {
        }

        public virtual void OnAfter(IInvocation invocation)
        {
        }

        public virtual void OnException(IInvocation invocation)
        {
        }

        public virtual void OnSuccess(IInvocation invocation)
        {
        }

        public override void Intercept(IInvocation invocation)
        {
            var isSuccess = true;

            OnBefore(invocation);
            try
            {
                invocation.Proceed();
            }
            catch
            {
                isSuccess = false;
                OnException(invocation);
                throw;
            }
            finally
            {
                if (isSuccess) OnSuccess(invocation);
            }

            OnAfter(invocation);
        }
    }
}