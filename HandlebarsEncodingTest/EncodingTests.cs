using HandlebarsDotNet;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace HandlebarsEncodingTest
{
    public partial class EncodingTests
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Handlebars_Should_Encode_Chinese(bool useAnnotations)
        {
            // Arrange
            RegisterPartialTemplates();
            var classTemplate = CompileClassTemplate();
            var data = GetTemplateData(useAnnotations);
            Handlebars.Configuration.NoEscape = true; // Do not escape Chinese characters

            // Act
            string actual = classTemplate(data);

            // Assert
            var expected = useAnnotations
                ? ExpectedEntitiesNoEscapeAnnotations.ProductClass
                : ExpectedEntitiesNoEscapeNoAnnotations.ProductClass;
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Handlebars_Should_Not_Encode_Chinese(bool useAnnotations)
        {
            // Arrange
            RegisterPartialTemplates();
            var classTemplate = CompileClassTemplate();
            var data = GetTemplateData(useAnnotations);
            Handlebars.Configuration.NoEscape = false; // Escape Chinese characters

            // Act
            string actual = classTemplate(data);

            // Assert
            var expected = useAnnotations
                ? ExpectedEntitiesEscapeAnnotations.ProductClass
                : ExpectedEntitiesEscapeNoAnnotations.ProductClass;
            Assert.Equal(expected, actual);
        }

        private Dictionary<string, object> GetTemplateData(bool useDataAnnotations)
        {
            var templateData = new Dictionary<string, object>();
            templateData.Add("use-data-annotations", useDataAnnotations);
            templateData.Add("namespace", "FakeNamespace");
            templateData.Add("class", "Product");
            templateData.Add("base-class", "EntityBase<int>");
            templateData.Add("comment", "    /// 产品");

            AddClassAnnotations(templateData, useDataAnnotations);
            AddClassProperties(templateData, useDataAnnotations);

            return templateData;
        }

        private void AddClassAnnotations(Dictionary<string, object> templateData, bool useDataAnnotations)
        {
            if (!useDataAnnotations) return;
            var classAnnotationsData = new List<Dictionary<string, object>>
            {
                new()
                {
                    { "class-annotation", "[Table(\"Product\")]" }
                },
                new()
                {
                    { "class-annotation", "[Index(nameof(CategoryId), Name = \"IX_Product_CategoryId\")]" }
                },
            };
            templateData.Add("class-annotations", classAnnotationsData);
        }

        private void AddClassProperties(Dictionary<string, object> templateData, bool useDataAnnotations)
        {
            var classPropertiesData = new List<Dictionary<string, object>>
            {
                new()
                {
                    { "property-type", "int" },
                    { "property-name", "ProductId" },
                    { "property-comment", "        /// 编号"},
                },
                new()
                {
                    { "property-type", "string" },
                    { "property-name", "ProductName" },
                    { "property-comment", "        /// 名称"},
                },
            };
            if (useDataAnnotations)
            {
                classPropertiesData[0].Add("property-annotations", new List<Dictionary<string, object>>
                {
                    new()
                    {
                        {"property-annotation", "[Key]"},
                    }
                });
                classPropertiesData[1].Add("property-annotations", new List<Dictionary<string, object>>
                {
                    new()
                    {
                        {"property-annotation", "[Required]"},
                    },
                    new()
                    {
                        {"property-annotation", "[StringLength(40)]"},
                    },
                    new()
                    {
                        {"property-annotation", "[Column(TypeName = \"money\")]"},
                    },
                });
            }
            templateData.Add("properties", classPropertiesData);
        }

        private void RegisterPartialTemplates()
        {
            var partialTemplates = GetPartialTemplates();
            foreach (var partialTemplate in partialTemplates)
            {
                Handlebars.RegisterTemplate(partialTemplate.Key, partialTemplate.Value);
            }
        }

        private Dictionary<string, string> GetPartialTemplates()
        {
            var ctorTemplateFile = File.ReadAllText("CodeTemplates/CSharpEntityType/Partials/Constructor.hbs");
            var importsTemplateFile = File.ReadAllText("CodeTemplates/CSharpEntityType/Partials/Imports.hbs");
            var propsTemplateFile = File.ReadAllText("CodeTemplates/CSharpEntityType/Partials/Properties.hbs");

            var partialTemplateFiles = new Dictionary<string, string>
            {
                { "constructor", ctorTemplateFile },
                { "imports", importsTemplateFile },
                { "properties", propsTemplateFile },
            };
            return partialTemplateFiles;
        }

        private HandlebarsTemplate<object, object> CompileClassTemplate()
        {
            var classTemplateFile = File.ReadAllText("CodeTemplates/CSharpEntityType/Class.hbs");
            return Handlebars.Compile(classTemplateFile);
        }
    }
}
