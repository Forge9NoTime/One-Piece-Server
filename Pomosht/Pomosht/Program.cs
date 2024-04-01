var animals = new Dictionary<string, Tuple<string, int>>();

HashSet<string> areas = new HashSet<string>();    

while (true)
{
    string s = Console.ReadLine();
    char[] separators = new char[] { ' ', '-' };

    string[] command = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);

    if (command[0] == "EndDay")
        break;
    else
    {

        if (command[0] == "Add:")
        {
            string animalName = command[1];
            int neededFood = int.Parse(command[2]);
            string area = command[3];

            if (animals.ContainsKey(animalName))
            {
                animals[animalName] = new Tuple<string, int>(area, animals[animalName].Item2 + neededFood);
            }
            else
            {
                animals.Add(animalName, new Tuple<string, int>(area, neededFood));
            }
            areas.Add(area);

        }
        else if (command[0] == "Feed:")
        {
            string animalName = command[1];
            int food = int.Parse(command[2]);

            if (animals.ContainsKey(animalName))
            {
                animals[animalName] = new Tuple<string,int>(animals[animalName].Item1, animals[animalName].Item2 - food);

                if (animals[animalName].Item2 <= 0)
                {

                    Console.WriteLine($"{animalName} was successfully fed");
                    animals.Remove(animalName);
                }
            }
            else
            {
                continue;
            }
        }
    }
}

Console.WriteLine("Animals:");
foreach (var animal in animals)
{
    Console.WriteLine($"{animal.Key} -> {animal.Value.Item2}g");
}

Console.WriteLine("Areas with hungry animals:");
foreach (var area in areas)
{
    var animalsInArea = animals.Where(a => a.Value.Item1 == area).ToList();
    if (animalsInArea.Count > 0)
    {
        Console.WriteLine($"{area}: {animalsInArea.Count}");
    }
}