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

    size_t n = count(pwd.begin(),pwd.end(), letter);

    if ((n >= min) && (n <= -max))
      c++;
  }

  cout << c;
  return 0;
}
