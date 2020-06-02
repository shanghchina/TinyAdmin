namespace Tiny.OPS.Repository
{
    public interface IUserRoleRepository
    {
        bool RemoveByUserId(long userId);
    }
}
