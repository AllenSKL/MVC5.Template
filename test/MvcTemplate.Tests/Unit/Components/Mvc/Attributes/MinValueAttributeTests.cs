﻿using MvcTemplate.Components.Mvc;
using MvcTemplate.Resources.Form;
using System;
using Xunit;
using Xunit.Extensions;

namespace MvcTemplate.Tests.Unit.Components.Mvc
{
    public class MinValueAttributeTests
    {
        private MinValueAttribute attribute;

        public MinValueAttributeTests()
        {
            attribute = new MinValueAttribute(12.56);
        }

        #region Constructor: MinValueAttribute(Int32 minimum)

        [Fact]
        public void MinValueAttribute_SetsMinimumFromInteger()
        {
            Decimal actual = new MinValueAttribute(10).Minimum;
            Decimal expected = 10M;

            Assert.Equal(expected, actual);
        }

        #endregion

        #region Constructor: MinValueAttribute(Double minimum)

        [Fact]
        public void MinValueAttribute_SetsMinimumFromDouble()
        {
            Decimal actual = new MinValueAttribute(12.56).Minimum;
            Decimal expected = 12.56M;

            Assert.Equal(expected, actual);
        }

        #endregion

        #region Method: FormatErrorMessage(String name)

        [Fact]
        public void FormatErrorMessage_FormatsErrorMessageForInteger()
        {
            attribute = new MinValueAttribute(10);

            String expected = String.Format(Validations.FieldMustBeGreaterOrEqualTo, "Sum", attribute.Minimum);
            String actual = attribute.FormatErrorMessage("Sum");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FormatErrorMessage_FormatsErrorMessageForDouble()
        {
            attribute = new MinValueAttribute(12.56);

            String expected = String.Format(Validations.FieldMustBeGreaterOrEqualTo, "Sum", attribute.Minimum);
            String actual = attribute.FormatErrorMessage("Sum");

            Assert.Equal(expected, actual);
        }

        #endregion

        #region Method: IsValid(Object value)

        [Fact]
        public void IsValid_NullValueIsValid()
        {
            Assert.True(attribute.IsValid(null));
        }

        [Theory]
        [InlineData(12.56)]
        [InlineData("12.561")]
        public void IsValid_GreaterOrEqualValueIsValid(Object value)
        {
            Assert.True(attribute.IsValid(value));
        }

        [Fact]
        public void IsValid_GreaterValueIsNotValid()
        {
            Assert.False(attribute.IsValid(12.559));
        }

        [Fact]
        public void IsValid_NotDecimalValueIsNotValid()
        {
            Assert.False(attribute.IsValid("12.56M"));
        }

        #endregion
    }
}
