#include <bits/stdc++.h>

using namespace std;

int main()
{
  ifstream f("day01.dat");

  if (!f.is_open())
    return -1;

  unordered_set<int> numbers;
  while (!f.eof()) {
    int d;
    f >> d;
    numbers.insert(d);
  }

  for (auto i : numbers)
    if (auto p = numbers.find(2020-i); p != numbers.end())
      cout << *p * i << endl;

  cout << "NAN";
  return 0;
}
