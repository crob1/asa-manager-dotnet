// Copyright (c) Microsoft. All rights reserved.

using System;
using System.Collections.Generic;
using Microsoft.Azure.IoTSolutions.AsaManager.Services.Models;
using Newtonsoft.Json;
using Services.Test.helpers;
using Xunit;

namespace Services.Test.Models
{
    public class RuleTest
    {
        [Fact, Trait(Constants.TYPE, Constants.UNIT_TEST)]
        public void EmptyInstancesAreEqual()
        {
            // Arrange
            var source = new Rule();
            var dest = new Rule();

            // Assert
            Assert.True(source.Equals(dest));
        }

        [Fact, Trait(Constants.TYPE, Constants.UNIT_TEST)]
        public void RulesDescriptionDoesntAffectEquality()
        {
            // Arrange
            var source = new Rule { Description = "one" };
            var dest = new Rule { Description = "two" };

            // Assert
            Assert.True(source.Equals(dest));
        }

        [Fact, Trait(Constants.TYPE, Constants.UNIT_TEST)]
        public void NonEmptyInstancesWithSameDataAreEqual()
        {
            // Arrange: rule without conditions
            var source = new Rule
            {
                Id = Guid.NewGuid().ToString(),
                Name = Guid.NewGuid().ToString(),
                Enabled = true,
                Description = Guid.NewGuid().ToString(),
                GroupId = Guid.NewGuid().ToString(),
                Severity = Guid.NewGuid().ToString(),
                Conditions = new List<Rule.Condition>()
            };
            var dest = Clone(source);

            // Assert
            Assert.True(source.Equals(dest));

            // Arrange: rule with conditions
            source = new Rule
            {
                Id = Guid.NewGuid().ToString(),
                Name = Guid.NewGuid().ToString(),
                Enabled = true,
                Description = Guid.NewGuid().ToString(),
                GroupId = Guid.NewGuid().ToString(),
                Severity = Guid.NewGuid().ToString(),
                Conditions = new List<Rule.Condition>
                {
                    new Rule.Condition { Field = "temp", Operator = ">=", Value = "75" },
                    new Rule.Condition { Field = "hum", Operator = "gt", Value = "50" },
                }
            };
            dest = Clone(source);

            // Assert
            Assert.True(source.Equals(dest));
        }

        [Fact, Trait(Constants.TYPE, Constants.UNIT_TEST)]
        public void InstancesWithDifferentDataAreDifferent()
        {
            // Arrange
            var source = new Rule
            {
                Id = Guid.NewGuid().ToString(),
                Name = Guid.NewGuid().ToString(),
                Enabled = true,
                Description = Guid.NewGuid().ToString(),
                GroupId = Guid.NewGuid().ToString(),
                Severity = Guid.NewGuid().ToString(),
                Conditions = new List<Rule.Condition>()
            };
            var dest1 = Clone(source);
            var dest2 = Clone(source);
            var dest3 = Clone(source);
            var dest4 = Clone(source);
            var dest5 = Clone(source);

            dest1.Id += "x";
            dest2.Name += "x";
            dest3.Enabled = !dest3.Enabled;
            dest4.GroupId += "x";
            dest5.Severity += "x";

            // Assert
            Assert.False(source.Equals(dest1));
            Assert.False(source.Equals(dest2));
            Assert.False(source.Equals(dest3));
            Assert.False(source.Equals(dest4));
            Assert.False(source.Equals(dest5));
        }

        [Fact, Trait(Constants.TYPE, Constants.UNIT_TEST)]
        public void InstancesWithDifferentConditionsAreDifferent()
        {
            // Arrange: different number of conditions
            var source = new Rule
            {
                Conditions = new List<Rule.Condition>()
            };
            var dest = Clone(source);
            dest.Conditions.Add(new Rule.Condition());

            // Assert
            Assert.False(source.Equals(dest));

            // Arrange: different field
            source.Conditions = new List<Rule.Condition>
            {
                new Rule.Condition { Field = "x", Operator = ">=", Value = "5" }
            };
            dest = Clone(source);
            dest.Conditions[0].Field = "y";

            // Assert
            Assert.False(source.Equals(dest));

            // Arrange: different operator
            dest = Clone(source);
            dest.Conditions[0].Operator = "<";

            // Assert
            Assert.False(source.Equals(dest));

            // Arrange: different value
            dest = Clone(source);
            dest.Conditions[0].Value = "123";

            // Assert
            Assert.False(source.Equals(dest));
        }

        private static T Clone<T>(T o)
        {
            return JsonConvert.DeserializeObject<T>(
                JsonConvert.SerializeObject(o));
        }
    }
}
