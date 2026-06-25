# Distributed Lab Test Task

This repository contains implementation of test tasks for Distributed Lab. Currently, two tasks has been implemented:
- Task #7 - Pinatas;
- Task #8 - Students Capital.

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
