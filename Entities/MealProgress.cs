namespace DietApp.Entities
{
    public class MealProgress
    {
        public int Id { get; set; }

        // Hangi diet listesine ait (ForeignKey)
        public int DietListId { get; set; }

        // Gün ismi örn: "Pazartesi"
        public string DayName { get; set; }

        // Dictionary üzerinde kullandığımız index veya meal unique kimliği
        public int MealIndex { get; set; }

        // Öğün tamamlandı mı?
        public bool IsCompleted { get; set; }

        // Ne zaman tamamlandı (opsiyonel)
        public DateTime? CompletedAt { get; set; }
    }
}
