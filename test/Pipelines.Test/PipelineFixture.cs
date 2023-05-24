//using Artech.Pipelines;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;

//namespace Pipelines.Test
//{
//    public class PipelineFixture
//    {
//        [Fact]
//        public async void PassThroughAsync()
//        { 
//            var pipeline = new Pipeline<Context>(new List<Pipe<Context>> { new Pipe("foo"), new Pipe("bar"), new Pipe("baz")});
//            _list.Clear();
//            await pipeline.PassThroughAsync(new Context());
//            Assert.Equal(3, _list.Count);
//            Assert.Equal("foo", _list[0]);
//            Assert.Equal("bar", _list[1]);
//            Assert.Equal("baz", _list[2]);
//        }

//        private static List<string> _list = new List<string>();
//        private class Context { }
//        private class Pipe : Pipe<Context>
//        {
//            public Pipe(string description)=> Description = description;
//            public override string Description { get; }
//            protected override ValueTask InvokeAsync(Context context, Pipeline<Context> next)
//            {
//                _list.Add(Description);
//                return next.PassThroughAsync(context);
//            }
//        }
//    }
//}
