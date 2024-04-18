using StudentsInfo.DataModels;
using StudentsInfo.Enums;
using System.ComponentModel.DataAnnotations;

namespace StudentsInfo
{
    public static class StudentCreator
    {
        public static List<Student> CreateDefaultStudents()
        {
            return new List<Student>()
            {
                new Student("Vlad", "Smith", "3609432642", "2B", new List<DisciplineModel> {
                    new DisciplineModel(DisciplineName.PE, Score.A,
                        new DateTime(1992, 2, 24), new DateTime(1993, 2, 23)),
                    new DisciplineModel(DisciplineName.Art, Score.C,
                        new DateTime(1992, 2, 22), new DateTime(1993, 2, 23))
                    }),

                new Student("David", "Young", "942345628", "2A", new List<DisciplineModel> {
                    new DisciplineModel(DisciplineName.Math, Score.E,
                        new DateTime(2002, 2, 11), new DateTime(2002, 6, 20)),
                    new DisciplineModel(DisciplineName.English, Score.C,
                        new DateTime(2001, 9, 15), new DateTime(2002, 1, 10)),
                    new DisciplineModel(DisciplineName.Art, Score.D,
                        new DateTime(2001, 9, 15), new DateTime(2002, 1, 10))
                    }),

                new Student("Andrew", "Gorestriker", "438912345", "3A", new List<DisciplineModel> {
                    new DisciplineModel(DisciplineName.History, Score.None,
                        new DateTime(2003, 2, 17), new DateTime(2003, 6, 18)),
                    new DisciplineModel(DisciplineName.English, Score.D,
                        new DateTime(2002, 9, 1), new DateTime(2003, 1, 11))
                    }),

                new Student("Ramiro", "Amos", "732942112", "3B", new List<DisciplineModel> {
                    new DisciplineModel(DisciplineName.Art, Score.None,
                        new DateTime(2003, 2, 15), new DateTime(2003, 6, 10)),
                    new DisciplineModel(DisciplineName.English, Score.D,
                        new DateTime(2002, 9, 1), new DateTime(2003, 1, 19)),
                    new DisciplineModel(DisciplineName.PE, Score.A,
                        new DateTime(2002, 9, 4), new DateTime(2003, 1, 12))
                    }),

                new Student("Walter", "Bozzelli", "654283745", "6C", new List<DisciplineModel> {
                    new DisciplineModel(DisciplineName.Art, Score.Fx,
                        new DateTime(2000, 2, 21), new DateTime(2001, 6, 18)),
                    new DisciplineModel(DisciplineName.Art, Score.C,
                        new DateTime(2001, 9, 24), new DateTime(2002, 1, 4)),
                    new DisciplineModel(DisciplineName.English, Score.D,
                        new DateTime(2000, 9, 17), new DateTime(2001, 1, 24)),
                    new DisciplineModel(DisciplineName.History, Score.B,
                        new DateTime(2000, 9, 15), new DateTime(2001, 1, 15))
                    })
            };
        }

        public static List<Student> CreateRandomStudents([Range(1, 10)] int range)
        {
            var result = new List<Student>();

            List<string> firstNames = new List<string>()
            {
                "Walter",
                "Richard",
                "Peter",
                "Orti",
                "Roger",
                "Alexander",
                "Harry",
                "Simon",
                "Fernando"
            };

            List<string> lastNames = new List<string>()
            {
                "Woodgrip",
                "Fellowes",
                "Mintz",
                "Ashbluff",
                "Marblemaw",
                "Bozzelli",
                "Windward",
                "Yearwood",
                "Oatrun"
            };

            List<string> phoneNumbers = new List<string>()
            {
                "38456217",
                "98517624",
                "57412346",
                "51679524",
                "46287423",
                "56971346",
                "56418792",
                "57134682",
                "42761588"
            };

            List<string> groupNumbers = new List<string>()
            {
                "3A",
                "4B",
                "2C",
                "2A",
                "2B",
                "4A",
                "3B",
                "3C",
                "1A"
            };

            Random r = new Random();
            for (int i = 0; i < range; i++)
            {
                int[] randomNumbers = [
                r.Next(9),
                r.Next(9 - i),
                r.Next(9 - i),
                r.Next(9)
                ];

                var disciplinesList = new List<DisciplineModel>();
                int disciplineCount = r.Next(1, 10);
                for (int j = 0; j < disciplineCount; j++)
                {
                    var startDate = new DateTime(r.Next(1990, 2024),
                                                 r.Next(1, 13),
                                                 r.Next(1, 28));

                    var endDate = new DateTime(startDate.AddYears(r.Next(1, 5)).Year,
                                               startDate.AddMonths(r.Next(6, 12)).Month,
                                               startDate.AddDays(r.Next(15, 30)).Day);

                    disciplinesList.Add(new DisciplineModel(
                        (DisciplineName)Enum.GetValues(typeof(DisciplineName)).GetValue(r.Next(1, 6)),
                        (Score)Enum.GetValues(typeof(Score)).GetValue(r.Next(1, 7)),
                        startDate,
                        endDate));
                }

                result.Add(new Student(firstNames[randomNumbers[0]],
                                       lastNames[randomNumbers[1]],
                                       phoneNumbers[randomNumbers[2]],
                                       groupNumbers[randomNumbers[3]],
                                       disciplinesList));

                lastNames.Remove(lastNames[randomNumbers[1]]);
                phoneNumbers.Remove(phoneNumbers[randomNumbers[2]]);
            }

            return result;
        }
    }
}
