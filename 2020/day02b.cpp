#include <bits/stdc++.h>

using namespace std;

int main()
{
  ifstream f("day02.dat");

  if (!f.is_open())
    return -1;

  int c = 0;
  while (!f.eof()) {
    int min, max;
    char letter, dot;
    string pwd;
    f >> min >> max >> letter >> dot >> pwd;

    bool a = (min <= pwd.length()) && pwd[min-1] == letter;
    bool b = (-max <= pwd.length()) && pwd[-max-1] == letter;

    if (a ^ b)
      c++;
  }

  cout << c;
  return 0;
}
