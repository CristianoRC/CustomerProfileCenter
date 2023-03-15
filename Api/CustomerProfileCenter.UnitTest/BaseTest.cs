using Bogus;
using Xunit;

namespace CustomerProfileCenter.UnitTest;

[Trait("Category", "Unit")]
public abstract class BaseTest
{
    protected Faker Faker => new Faker("pt_BR");
}