using Desktop;

namespace UTest
{
    public class ValidatorTest
    {
        [Test]
        public void ValidatePassword()
        {
            Assert.That(Validator.ValidatePassword("12345"));
        }
        [Test]
        public void ValidateEmail()
        {
            Assert.That(Validator.ValidateEmail("testemail123@mail.ru"));
        }
    }
}