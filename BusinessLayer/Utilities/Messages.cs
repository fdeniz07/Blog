namespace BusinessLayer.Utilities
{
    public static class Messages
    {
        // Messages.Category.NotFound();

        public static class Category // Buradaki Category sinifi entity bölümünden farklidir
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Hiç bir kategori bulunamadı.";
                return "Böyle bir kategori bulunamadı.";
            }

            public static string Add(string categoryName)
            {
                return $"{categoryName} adlı kategori başarıyla eklenmiştir";
            }

            public static string Update(string categoryName)
            {
                return $"{categoryName} adlı kategori başarıyla güncellenmiştir.";
            }

            public static string Delete(string categoryName)
            {
                return $"{categoryName}  adlı kategori başarıyla silinmistir.";
            }

            public static string HardDelete(string categoryName)
            {
                return $"{categoryName} adlı kategori veritabanindan başarıyla silinmiştir.";
            }
        }

        public static class Blog // Buradaki Blog sinifi entity bölümünden farklidir
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Makaleler bulunamadı.";
                return "Böyle bir makale bulunamadı.";
            }

            public static string Add(string title)
            {
                return $"{title} başlıklı makale başarıyla eklenmiştir.";
            }

            public static string Update(string title)
            {
                return $"{title} baslikli makale başariyla güncellenmiştir.";
            }

            public static string Delete(string title)
            {
                return $"{title}  başlıklı makale başarıyla silinmiştir.";
            }

            public static string HardDelete(string title)
            {
                return $"{title} başlıklı makale veritabanından başarıyla silinmiştir.";
            }
        }
    }
}
