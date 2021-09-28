# Handlebars-Net Encoding Test

Test encoding of Chinese characters with Handlebars-Net

## Scenario 1: NoEscape = True

_Set Configuration.NoEscape to True_

### A. Without Data Annotations in Template Data

   * **Expected:**

```csharp
using System;
using System.Collections.Generic;

namespace FakeNamespace
{
    /// <summary>
    /// 产品
    /// </summary>
    public partial class 
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
```

   * **Actual:**

_Test passed: Actual output matches expected. No comments are escaped._

```csharp
using System;
using System.Collections.Generic;

namespace FakeNamespace
{
    /// <summary>
    /// 产品
    /// </summary>
    public partial class 
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
```

### B. With Data Annotations in Template Data

   * **Expected:**

```csharp
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FakeNamespace
{
    /// <summary>
    /// 产品
    /// </summary>
    [Table("Product")]
    [Index(nameof(CategoryId), Name = "IX_Product_CategoryId")]
    public partial class 
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
        [Column(TypeName = "money")]
        public string ProductName { get; set; }
    }
}
```

   * **Actual:**

_Test failed: Comment on ProductName property IS escaped (when it should not be)._

```csharp
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FakeNamespace
{
    /// <summary>
    /// 产品
    /// </summary>
    [Table("Product")]
    [Index(nameof(CategoryId), Name = "IX_Product_CategoryId")]
    public partial class 
    {

        /// <summary>
        /// 编号
        /// </summary>
        [Key]
        public int ProductId { get; set; }

        /// <summary>
        /// &#21517;&#31216;
        /// </summary>
        [Required]
        [StringLength(40)]
        [Column(TypeName = "money")]
        public string ProductName { get; set; }
    }
}
```

## Scenario 2: NoEscape = False

_Set Configuration.NoEscape to False_

### A. Without Data Annotations in Template Data

   * **Expected:**

```csharp
using System;
using System.Collections.Generic;

namespace FakeNamespace
{
    /// <summary>
    /// &#20135;&#21697;
    /// </summary>
    public partial class 
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
```

   * **Actual:**

_Test failed: Properties are NOT escaped (when they should be)._

```csharp
using System;
using System.Collections.Generic;

namespace FakeNamespace
{
    /// <summary>
    /// &#20135;&#21697;
    /// </summary>
    public partial class 
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
```

### B. With Data Annotations in Template Data

   * **Expected:**

```csharp
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FakeNamespace
{
    /// <summary>
    /// &#20135;&#21697;
    /// </summary>
    [Table("Product")]
    [Index(nameof(CategoryId), Name = "IX_Product_CategoryId")]
    public partial class 
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
        [Column(TypeName = "money")]
        public string ProductName { get; set; }
    }
}
```

   * **Actual:**

_Test failed: ProductId property is NOT escaped (when it should be)._

```csharp
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FakeNamespace
{
    /// <summary>
    /// &#20135;&#21697;
    /// </summary>
    [Table("Product")]
    [Index(nameof(CategoryId), Name = "IX_Product_CategoryId")]
    public partial class 
    {

        /// <summary>
        /// 编号
        /// </summary>
        [Key]
        public int ProductId { get; set; }

        /// <summary>
        /// &#21517;&#31216;
        /// </summary>
        [Required]
        [StringLength(40)]
        [Column(TypeName = "money")]
        public string ProductName { get; set; }
    }
}
```

