namespace HandlebarsEncodingTest
{
    public partial class EncodingTests
    {
        private static class ExpectedEntitiesNoEscapeNoAnnotations
        {
            public const string ProductClass =
                @"using System;
using System.Collections.Generic;

namespace FakeNamespace
{
    /// <summary>
    /// 产品
    /// </summary>
    public partial class Product : EntityBase<int>
    {

        /// <summary>
        /// 编号
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string ProductName { get; set; }
    }
}
";
        }

        private static class ExpectedEntitiesNoEscapeAnnotations
        {
            public const string ProductClass =
                @"using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FakeNamespace
{
    /// <summary>
    /// 产品
    /// </summary>
    [Table(""Product"")]
    [Index(nameof(CategoryId), Name = ""IX_Product_CategoryId"")]
    public partial class Product : EntityBase<int>
    {

        /// <summary>
        /// 编号
        /// </summary>
        [Key]
        public int ProductId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(40)]
        [Column(TypeName = ""money"")]
        public string ProductName { get; set; }
    }
}
";
        }

        private static class ExpectedEntitiesEscapeNoAnnotations
        {
            public const string ProductClass =
                @"using System;
using System.Collections.Generic;

namespace FakeNamespace
{
    /// <summary>
    /// &#20135;&#21697;
    /// </summary>
    public partial class Product : EntityBase<int>
    {

        /// <summary>
        /// &#32534;&#21495;
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// &#21517;&#31216;
        /// </summary>
        public string ProductName { get; set; }
    }
}
";
        }

        private static class ExpectedEntitiesEscapeAnnotations
        {
            public const string ProductClass =
                @"using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FakeNamespace
{
    /// <summary>
    /// &#20135;&#21697;
    /// </summary>
    [Table(""Product"")]
    [Index(nameof(CategoryId), Name = ""IX_Product_CategoryId"")]
    public partial class Product : EntityBase<int>
    {

        /// <summary>
        /// &#32534;&#21495;
        /// </summary>
        [Key]
        public int ProductId { get; set; }

        /// <summary>
        /// &#21517;&#31216;
        /// </summary>
        [Required]
        [StringLength(40)]
        [Column(TypeName = ""money"")]
        public string ProductName { get; set; }
    }
}
";
        }
    }
}