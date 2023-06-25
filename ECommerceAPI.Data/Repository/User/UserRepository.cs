namespace ECommerceAPI.Data.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(EFContext context) : base(context)
        {

        }
        public User GetUsername(string userName)
        {
            return dbContext.Set<User>().Where(x => x.UserName == userName).FirstOrDefault();
        }
    }
}
