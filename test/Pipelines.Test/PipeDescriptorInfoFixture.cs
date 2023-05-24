using Artech.Pipelines;
using Xunit;

namespace Pipelines.Test
{
    public class PipeDescriptorInfoFixture
    {
        [Fact]
        public void Format()
        {
            var baz = new PipeDescriptorInfo("Baz", PipeDescriptorInfo.Terminal);
            var bar = new PipeDescriptorInfo("Bar", baz);

            var bar3 = new PipeDescriptorInfo("Bar3", PipeDescriptorInfo.Terminal);
            var bar2 = new PipeDescriptorInfo("Bar2", bar3);
            var bar1 = new PipeDescriptorInfo("Bar1", bar2);
            bar.SubPipeline = bar1;

            var foo = new PipeDescriptorInfo("Foo", bar);
            Assert.Equal("Foo\r\nBar\r\n\tBar1\r\n\tBar2\r\n\tBar3\r\nBaz\r\n", foo.ToString());
        }
    }
}