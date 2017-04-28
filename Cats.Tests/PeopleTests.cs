using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Autofac.Features.OwnedInstances;
using Autofac.Util;
using Cats.API;
using Cats.Models;
using Cats.Services;
using Cats.Tests.Fakes;
using Newtonsoft.Json;
using NSubstitute;

using Xunit;

namespace Cats.Tests
{
    public class PeopleTests
    {
        private static IJsonService SetupService()
        {
            var json =
                @"[{""name"":""Bob"",""gender"":""Male"",""age"":23,""pets"":[{""name"":""Garfield"",""type"":""Cat""},{""name"":""Fido"",""type"":""Dog""}]},{""name"":""Jennifer"",""gender"":""Female"",""age"":18,""pets"":[{""name"":""Garfield"",""type"":""Cat""}]},{""name"":""Steve"",""gender"":""Male"",""age"":45,""pets"":null},{""name"":""Fred"",""gender"":""Male"",""age"":40,""pets"":[{""name"":""Tom"",""type"":""Cat""},{""name"":""Max"",""type"":""Cat""},{""name"":""Sam"",""type"":""Dog""},{""name"":""Jim"",""type"":""Cat""}]},{""name"":""Samantha"",""gender"":""Female"",""age"":40,""pets"":[{""name"":""Tabby"",""type"":""Cat""}]},{""name"":""Alice"",""gender"":""Female"",""age"":64,""pets"":[{""name"":""Simba"",""type"":""Cat""},{""name"":""Nemo"",""type"":""Fish""}]}]";

            var jsonService = Substitute.For<IJsonService>();
            var people = JsonConvert.DeserializeObject<List<Person>>(json);
            
            jsonService.GetPeople().Returns(people);

            return jsonService;
        }

        [Fact]
        public async Task Json_Service_Returns_People()
        {
            // arrange
            var jsonService = SetupService();
            
            // act
            var people = jsonService.GetPeople().Result;

            // assert
            Assert.True(people.Any());
            
        }
    }
}
