using Messages.Core;
using Router.Resequencers;

namespace Router.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var sequence = new InMemoryNumericResequencer<TestMessage>();

            sequence.Add(new TestMessage(){ SequenceId = 1});
            sequence.Add(new TestMessage(){ SequenceId = 4});
            sequence.Add(new TestMessage(){ SequenceId = 5});
            sequence.Add(new TestMessage(){ SequenceId = 2 });

            var numbers = sequence.ExtractCompletedSegment();


        }
    }

    public class TestMessage : IHaveSequenceId<long>
    {
        public long SequenceId { get; set; }
    }
}