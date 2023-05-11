using System.Collections.Generic;

namespace VTTiny.Base
{
    /// <summary>
    /// This class is responsible for allocating IDs.
    /// </summary>
    public class IdAllocator
    {
        /// <summary>
        /// This contains all of the returned IDs we can allocate.
        /// </summary>
        private readonly Stack<int> _freeIds;

        /// <summary>
        /// This contains the last used ID.
        /// </summary>
        private int _lastUsedId = -1;

        /// <summary>
        /// Create a new asset id allocator.
        /// </summary>
        public IdAllocator(int lastId = -1)
        {
            _freeIds = new();
            _lastUsedId = lastId;
        }

        /// <summary>
        /// Allocates a new asset id.
        /// </summary>
        /// <returns>The new asset id.</returns>
        public int AllocateId()
        {
            if (_freeIds.TryPop(out int usedId))
                return usedId;

            _lastUsedId++;
            return _lastUsedId;
        }

        /// <summary>
        /// Frees a used id.
        /// </summary>
        /// <param name="id">The id to free.</param>
        public void FreeId(int id)
        {
            _freeIds.Push(id);
        }
    }
}
