using StudentsInfo.DataModels;

namespace DesignPattern_Factory.Factories
{
    internal abstract class BaseFactoryMethod
    {
        protected abstract BaseUser CreateUser(int userId);

        public BaseUser GetUser(int userId)
        {
            return CreateUser(userId);
        }
    }
}
