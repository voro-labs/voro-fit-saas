namespace VoroFit.Shared.Helpers
{
    public static class CollectionSyncHelper
    {
        public static void Sync<TDb, TDto>(
            ICollection<TDb> dbItems,
            IEnumerable<TDto> dtoItems,
            Func<TDb, Guid> dbId,
            Func<TDto, Guid> dtoId,
            Func<TDto, TDb> create,
            Action<TDb, TDto> update,
            Action<TDb> softDelete
        )
        {
            var dtoList = dtoItems?.ToList() ?? [];
            var dbList = dbItems.ToList();

            foreach (var dbItem in dbList)
            {
                if (!dtoList.Any(dto => dtoId(dto) == dbId(dbItem)))
                {
                    softDelete(dbItem);
                }
            }

            foreach (var dto in dtoList)
            {
                if (dtoId(dto) == Guid.Empty)
                {
                    dbItems.Add(create(dto));
                }
                else
                {
                    var dbItem = dbList.First(db => dbId(db) == dtoId(dto));
                    update(dbItem, dto);
                }
            }
        }
    }
}
