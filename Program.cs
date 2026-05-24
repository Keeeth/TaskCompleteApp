namespace TaskCompleteApp
{
    class Program
    {
        // This is your permanent database file name
        static string filePath = "taskHistory.txt";
        
        
        static List<string> taskHistory = new List<string>();

        static void Main(string[] args)
        {
            // 1. Pull existing data from the file so the app "remembers"
        LoadFromDatabase();
    


            bool running = true;
            while (running)
            {
                Console.WriteLine("\n--- TaskComplete Main Menu ---");
                Console.WriteLine("1. Add New Task");
                Console.WriteLine("2. View History Table");
                Console.WriteLine("3. Exit");
                Console.WriteLine("4. Update Task Status");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter task (e.g., 2 job applications): ");
                        string task = Console.ReadLine() ?? "Unnamed Task";
                       // Manually set colors for the "Selection" labels
                          Console.ForegroundColor = ConsoleColor.Green;
                          Console.Write("Status: (1) Completed "); 
                          Console.ResetColor(); // Reset so the comma/next text isn't green
                          Console.ForegroundColor = ConsoleColor.Yellow;
                          Console.WriteLine("(2) Pending"); 
                          Console.ResetColor();
                           Console.Write("Select status (1 or 2): ");
                          string statusChoice = Console.ReadLine() ?? "";
                        // This logic assigns the status based on your choice
                          string status = (statusChoice == "2") ? "Pending" : "Completed";    
                           LogTask(task, status);

                    // Apply color to the final confirmation message using a condition
                     Console.ForegroundColor = (status == "Completed") ? ConsoleColor.Green : ConsoleColor.Yellow;
                     Console.WriteLine($"Task added as {status} and saved to history!");
                     Console.ResetColor(); 
                     break;
                        case "2":
                        DisplayHistoryTable();
                        break;
                        case "3":
                        Console.WriteLine("Closing TaskComplete. Great job today, Keith!");
                        running = false;
                        break;
                      case "4":
                        
    Console.WriteLine("\n--- SELECT A TASK BY INDEX ---");
    for (int i = 0; i < taskHistory.Count; i++)
    {
        Console.WriteLine($"[{i}] {taskHistory[i]}");
    }

    Console.Write("\nEnter index to update: ");
    if (int.TryParse(Console.ReadLine(), out int userInput))    
    {
        int index = userInput - 1;
        if(index >=0 && index < taskHistory.Count)
        {
        
        if (taskHistory[index].Contains("Pending"))
        {
            taskHistory[index] = taskHistory[index].Replace("Pending", "Completed");
            
            // PERSISTENCE: This saves the change back to your hard drive
            File.WriteAllLines(filePath, taskHistory); 
            
            Console.WriteLine("Dopamine Hit! Task marked as Completed.");           
        }
        // 2. If it's not Pending, check if it's currently Completed
             else if (taskHistory[index].Contains("Completed"))
        {
           taskHistory[index] = taskHistory[index].Replace("Completed", "Pending");
           Console.WriteLine("Task rolled back to Pending!");
              // PERSISTENCE: This saves the change back to your hard drive
                File.WriteAllLines(filePath, taskHistory);
       }
        else {
            Console.WriteLine("Invalid Selection number , Please try again!");
        }
    }
        break; 

              default:
                Console.WriteLine("Invalid selection. Please try again.");
                        break;
                }
            }
        }

        // This method now writes directly to your hard drive
        static void LogTask(string taskName, string status)
        {
            string date = DateTime.Now.ToString("MM/dd/yyyy");
            string entry = $"{date,-12} | {taskName,-35} | {status}";
            taskHistory.Add(entry);

            // This line creates 'taskHistory.txt' if it doesn't exist and adds the line
            File.AppendAllLines(filePath, new List<string> { entry });
        }

        // This pulls your history back into the app when you start it
        static void LoadFromDatabase()
        {
            if (File.Exists(filePath))
            {
                taskHistory = new List<string>(File.ReadAllLines(filePath));
            }
        }

        static void DisplayHistoryTable()
        {
            Console.WriteLine("\n--- HISTORICAL TASK LOG ---");
            Console.WriteLine("{0,-12} | {1,-35} | {2}", "DATE", "TASK DESCRIPTION", "STATUS");
            Console.WriteLine(new string('-', 65));

            if (taskHistory.Count == 0)
            {
                Console.WriteLine("No history found.");
            }
        else
        {
            foreach (var entry in taskHistory)
                {
                    // Because your history is currently just List<string>, 
                    // we have to split the string back into parts to check the color.
                    var parts = entry.Split('|');
                    if (parts.Length >=3)
                    {
                        
                    
                    string date = parts[0].Trim();
                    string name = parts[1].Trim();
                    string status = parts[2].Trim();

                    // 1. Align the text
                    Console.Write($"{date,-12} | {name,-35} | ");

                    // 2. The Conditional Color Method
                    if (status == "Completed")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                    }

                    // 3. Print the Status and Reset
                    Console.Write(status);
                    Console.ResetColor();

                    // 4. Move to the next line
                    Console.WriteLine();
                }
            }
        }
    }
   }
}