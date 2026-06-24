# Distributed Lab Test Task

This repository contains implementation of test tasks for Distributed Lab. Currently, one task has been implemented:
- Task #6 - Pinatas.

## Task: Pinatas

A console application that calculates the max amount of candies you would have by smashing the pinatas wisely. The algorithm uses the Dynamic programming paradigm with the Memorization technique to optimize calculations.

How to run:
1. Make sure you have the NET 8.0 SDK installed.
2. Clone this repository.
3. open the Tasks solution file in your favourite development enviroment (e.g., Visual Studio 2022 or JetBrains Rider).
4. Set the Pinatas project as the Startup Project.
5. Run the project.
6. In the console, enter an array of numbers separated by spaces (no punctuation) and press Enter.

Big O Notation

The program uses the Divide and Conquer approach with caching of intermediate results in a two-dimentional array _memo.

**Time Complexity: O(N^3)**

wherre N - is the number of pinatas.
The number of unique subtasks stored in the cache is O(N^2). To compute each subtask, the algorithm iterates over the elements between the left and right bounds, which in the worst case takes O(N) steps. Overall complexity: O(N^2) * O(N) = O(N^3). Thanks fro memorization, we avoid the exponential complexity of a brute-force search, which makes the algorithm efficient even for large arrays.

**Space Complexity: O(N^2)**

A two-dimentional array _memo of size O(N^2) * O(N^2) requires O(N^2) of memory. The recursive call stack takes up O(N)  of memory per level. Overall, the space complexity is dominated by the memorization array and is O(N^2).

Additional information
- to protect against input errors, validation is implemented using int.TryParse.
- the task took 2 hours to complete.
