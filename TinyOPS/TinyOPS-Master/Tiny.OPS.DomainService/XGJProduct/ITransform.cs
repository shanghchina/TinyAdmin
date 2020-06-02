using Tiny.OPS.Domain.XGJProduct;

namespace Tiny.OPS.DomainService
{
    public interface ITransform
    {
        void ExecuteTrans(T_EXT_SyncHistory entity);
    }
}
