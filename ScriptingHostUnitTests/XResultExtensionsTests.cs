using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScriptingHost;

namespace ScriptingHostUnitTests
{
    [TestClass]
    public class XResultExtensionsTests
    {
        [TestMethod]
        public void OnSuccessTest()
        {
            var tuple = new Tuple<object, Exception>(null, null);
            Exception ex = null;
            tuple.OnSuccess(Assert.IsNull).OnError(
                e =>
                {
                    ex = e;
                    Assert.Fail();
                }
            );
            Assert.IsNull(ex);
        }
        
        [TestMethod]
        public void OnErrorTest()
        {
            var tuple = new Tuple<object, Exception>(null, new Exception());
            Exception ex = null;
            tuple.OnSuccess(o => Assert.Fail()).OnError(
                e =>
                {
                    ex = e;
                    Assert.IsNotNull(e);
                }
            );
            Assert.IsNotNull(ex);
        }
    }
}
