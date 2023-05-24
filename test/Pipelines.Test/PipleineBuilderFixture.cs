using Artech.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Pipelines.Test
{
    public class PipleineBuilderFixture
    {
        [Fact]
        public async void Build()
        {
            var pipeline = new PipelineBuilder<Context>()
                .Use(new Pipe("foo"))
                .Use(new Pipe("bar"))
                .Use(new Pipe("baz"))
                .Build(out var descriptor);
            _list.Clear();
            await pipeline(new Context());
            Assert.Equal(3, _list.Count);
            Assert.Equal("foo", _list[0]);
            Assert.Equal("bar", _list[1]);
            Assert.Equal("baz", _list[2]);
            Assert.Equal("foo\r\nbar\r\nbaz\r\n", descriptor.ToString());
        }

        private static List<string> _list = new List<string>();
        private class Context { }
        private class Pipe : Pipe<Context>
        {
            public Pipe(string description) => Description = description;
            public override string Description { get; }

            public override ValueTask InvokeAsync(Context context, Func<Context, ValueTask> next)
            {
                _list.Add(Description);
                return next(context);
            }
        }
    }
}
