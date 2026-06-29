# Distributed Lab Test Task

This repository contains implementation of test tasks for Distributed Lab. Currently, tree tasks has been implemented:
- Task #7 - Pinatas;
- Task #8 - Students Capital;
- Task #2 - Maze.
- Task #3 - Website analitycs.

## Task: Pinatas

A console application that calculates the max amount of candies you would have by smashing the pinatas wisely. The algorithm uses the Dynamic Programming paradigm with the Memorization technique to optimize calculations.

**How to run:**
1. Make sure you have the NET 8.0 SDK installed.
2. Clone this repository.
3. open the Tasks solution file in your favourite development enviroment (e.g., Visual Studio 2022 or JetBrains Rider).
4. Set the Pinatas project as the Startup Project.
5. Run the project.
6. In the console, enter an array of numbers, separated by spaces, without any punctuation marks and press Enter.

**Big O Notation**

The program uses the Divide and Conquer approach with caching of intermediate results in a two-dimentional array _memo.

**Time Complexity: O(N^3)**

wherre N - is the number of pinatas.
The number of unique subtasks stored in the cache is O(N^2). To compute each subtask, the algorithm iterates over the elements between the left and right bounds, which in the worst case takes O(N) steps. Overall complexity: O(N^2) * O(N) = O(N^3). Thanks fro memorization, we avoid the exponential complexity of a brute-force search, which makes the algorithm efficient even for large arrays.

**Space Complexity: O(N^2)**

A two-dimentional array _memo of size O(N^2) * O(N^2) requires O(N^2) of memory. The recursive call stack takes up O(N)  of memory per level. Overall, the space complexity is dominated by the memorization array and is O(N^2).

Additional information
- to protect against input errors, validation is implemented using int.TryParse.
- the task took 2 hours to complete.


## Task: Students Capital

A console application that calculates the maximum amount of capital a student can earn over the summer by byuing, fixing, and selling laptops. To solve this problem, the Greedy Algorithm was used: at each step the program searches for the laptop that is avalaible with the current capital and will iyeld the highest profit.

**How to run:**
1. Open the Tasks solution file in your IDE.
4. Set the StudentsCapital as the Startup Project.
5. Run the project.
6. Follow the instructions in the console. Press Enter after entering each answer. The program will ask you to enter the following in sequence:
 - Maximum numbers of laptop you can buy (N);
 - Start-up capital (C);
 - Total number of avalaible laptops (K);
 - An array of prices (prices) (separated by spaces, without any punctuation marks!)
 - An array of expected profits (gains) (separated by spaces, without any punctuation marks!)

**Big O Notation**

**Time Complexity: O(N*K)**

where N - is the limit on the number of laptops we can repair,
and K is the total number of laptops on the market.
The algorithm uses a nested loop: the outside loop rums N times, while the inner loop scans the entrie array K to find the most profitable curchase that our current capital allows. The overall complexity is O(N*K).

**Space Complexity: O(K)**

The program uses two one-deminsional arrays (prices ans gains), each of weach has a size of K. No additioal complex data structures are created, so memory usage is minimal and scales linearly with the number of laptops available on the market. 

Additional information
- all logic is divided into separate classes in accordance with the Single Responsibility Principle.
- interactive data entry is protected by an infinite loop, which prevents the program from crashing when text, empty string, or en incorrect numbernof arguments are entered, and require the user to enter data correctly without the risk of a StackOverflowException.
- time spent on the task: 3 hours.


## Task: Maze

The program generates random mazes of a specified size with a guaranteed path from the entrance (E) to the exit (X), and also randomly places treasure (T) and traps (P).

**How to run:**
1. Open the Tasks solution file in your IDE.
2. Set the Maze project as the Startup Project.
3. Run the project.
4. Enter the width and height of the Maze.
5. Press Enter.
Controls: arrow keys (Up, Down, Left, Right). The player is represented by the symbol 'O'.
Objective: collect the treasure (if present; optional), avoid stepping on 3 traps, and reach the exit.

The implementation uses similar to MVC pattern, that separates the generation logic (MazeGenerator), rendering (OutputData), safe input (InputData), and the game loop (GameEngine).

The followong algorithms were used in the development of the program:
1. Maze generation - DFS:
   depth-first search with a step size of 2 cells is used to create the perfect maze without loops, where the wisth of walls and path is always 1 cell.
2. Passability check and finding the shortest path - BFS:
   breadth-first search that the player can always reach the exit. The algirithm finds the shortest path and controls the placnebt of traps: there no more than 2 traps on the single correct path (death occurs after 3), while the remaining traps are scattered across the wrong branches.

**Big O Notation**

Let 'V' be the total munber of cells inthe maze matrix (width * height)

**Time Complexity: O(V)**

During maze generation, the DFS algorithm visits each cell a constant number of times. The BFS algorithm for finding the shortest path also scans the cells of the matrix at most once. The subsequent placment of treasure and traps takes O(V) time or less (via a random search of avalaible paths). The overall time complexity is linearly proportional to the area of the maze - O(V).

**Space Complexity: O(V)**

To run the program, a two-dimensional array of type string[,] of size width * height is created. The BFS algorithm also uses a Queue and an array of visited cells 'visited', which, in the worst case, scale proportionally to the size of the maze. Therefore, memory usage is O(V).

Time spent on the task: 12 hours.

## Task: Website Analytics

This application designed to analyze user behavior on an e-commerce website. The marketing department requires a tool to process visit logs from two separate days and identify a specific target audience. 

The application parses two CSV files and finds all users who meet both of the following criteria:
1. Visited some pages on both days;
2. On the second day visited the page that hadn’t been visited by this user on the first day.


**How to Run**

1. Clone the repository and open the solution Tasks.sln in your IDE.
2. Set the WebsiteAnalytics project as the Startup Project.
3. Ensure that the input files (firstDay.txt and secondDay.txt) are placed in the root of the project.
   File format structure: user_id,product_id,timestamp
4. Run the project. The console will output the list of user_id`s that match the marketing criteria.

**Big O Notation**

The algorithm was designed to handle large datasets efficiently, optimizing both execution time and memory storage.

**Time Complexity: O(N + M)**

N - number of rows in the first day's file.

M - number of rows in the second day's file.

The solution:
  1. Iterates through the first file once (O(N)).
  2. Iterates through the second file once (O(M)).
- Checking if a user exists and if a product was visited relies on Dictionary<TKey, TValue> and HashSet<T>. Both of these data structures provide O(1) complexity for lookups. Therefore, the total execution time scales linearly with the size of the input data.

**Space Complexity: O(U * P)**

U - number of unique users on the first day.

P - average number of unique products visited per user.

The program reads the files line-by-line as a stream. It never loads the entire file into RAM, drastically reducing memory usage, making it capable of processing multi-gigabyte files.
The only data stored in memory is the day1Visits dictionary and the targetUsers set. Because we use HashSet<string>, duplicate visits to the exact same page by the same user are automatically discarded and not stored in memory.

Time spent on the task: 5 hours.
