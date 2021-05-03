using System.Collections.Generic;
using Core.Extensions;
using Entities.Concrete;
using Xunit;

namespace UnitTest.Core.Extensions
{
    public class JSonExtensionsTests
    {
        [Fact]
        public void ToJson_Serialize_String()
        {
            var result = new Person {Id = 1, FirstName = "Yaren"}.ToJson();
            Assert.Contains("Yaren", result);
        }

        [Fact]
        public void FromJson_Deserialize_Model()
        {
            var persons = new List<Person>
            {
                new()
                {
                    Id = 1,
                    FirstName = "Yaren",
                    LastName = "KURT"
                }
            };
            
            var json = persons.ToJson();
            var result = json.FromJson<List<Person>>();
            Assert.True(result.Count ==1);
        }
    }
}