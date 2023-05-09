global using Xunit;
using BTreeFour;
namespace NodeFacts
{
    public class TestProgram
    {
        [Fact]
        public void AddKey_SortingIsDoneAutomatically()
        {
            Node<int> node = new();
            node.AddKey(3);
            node.AddKey(2);
            node.AddKey(1);
            Assert.Equal(new int[] { 1, 2, 3 }, node.Keys);
        }

        [Fact]
        public void RemoveKey_ItemIsRemovedAnd_OrderIsPreserved()
        {
            Node<int> node = new();
            node.AddKey(3);
            node.AddKey(2);
            node.AddKey(1);
            node.RemoveKey(2);
            Assert.Equal(new int[] { 1, 3, 0 }, node.Keys);
        }

        [Fact]
        public void ClearKeys_EverythingIsRemoved()
        {
            Node<int> node = new();
            node.AddKey(3);
            node.AddKey(2);
            node.AddKey(1);
            node.ClearKeys();
            Assert.Equal(node.KeysCount, 0);
        }

        [Fact]
        public void DeleteKey_ReturnsFalseIfItDoesntHaveExtraKeys()
        {
            Node<int> node = new();
            node.AddKey(2);
            node.AddKey(1);
            Assert.True(node.DeleteKey(2));
            Assert.False(node.DeleteKey(1));
        }

        [Fact]
        public void RemoveChild_ChildrenAreRedistributed()
        {
            BTreeFourthOrder<int> bTree = new();
            bTree.Insert(5);
            bTree.Insert(3);
            bTree.Insert(21);
            bTree.Insert(1);
            bTree.Insert(4);
            bTree.Insert(22);
            Assert.Equal(bTree.Root.CountChildren(), 3);           
            bTree.Root.RemoveChild(0);
            Assert.Equal(bTree.Root.CountChildren(), 2);
            Assert.NotEqual(bTree.Root.Children[0], null);            
        }

        [Fact]
        public void MergeParentAndBrotherInItsPlace_LeftBrother()
        {
            BTreeFourthOrder<int> bTree = new();
            bTree.Insert (1);
            bTree.Insert(2);
            bTree.Insert(3);
            bTree.Insert(4);
            bTree.Insert(5);
            bTree.Insert(6);
            bTree.Root.Children[2].RemoveKey(6);
            bTree.Root.Children[1].MergeParentAndBrotherInItsPlace();
            Assert.Equal( new int[] { 1, 2, 0 }, bTree.Root.Children[0].Keys);
        }

        [Fact]
        public void MergeParentAndBrotherInItsPlace_RightBrother()
        {
            BTreeFourthOrder<int> bTree = new();
            bTree.Insert(5);
            bTree.Insert(3);
            bTree.Insert(21);
            bTree.Insert(1);
            bTree.Insert(4);
            bTree.Insert(22);
            bTree.Insert(23);
            bTree.Insert(24);
            bTree.Insert(25);
            bTree.Insert(26);
            bTree.Remove(21);
            Assert.Equal(new int[] { 22, 23, 0 }, bTree.Root.Children[1].Children[0].Keys);
        }

        [Fact]
        public void FindInOrderSuccessor_ForRoot()
        {
            BTreeFourthOrder<int> bTree = new();
            bTree.Insert(1);
            bTree.Insert(2);
            bTree.Insert(3);
            bTree.Insert(4);
            Assert.True(bTree.Remove(2));
        }

        [Theory]
        [InlineData(0, 21)]
        [InlineData(1, 23)]
        public void InsertChild_ItSortsItself(int index, int key)
        {
            Node<int> node = new();
            Node<int> childNode = new();
            childNode.AddKey(23);
            Node<int> childNode2 = new();
            childNode2.AddKey(21);
            node.InsertChild(childNode);
            node.InsertChild(childNode2);
            Assert.True(node.Children[index].Keys[0] == key);
        }
    }
}