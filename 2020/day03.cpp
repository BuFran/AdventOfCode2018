#include <bits/stdc++.h>

using namespace std;

int main()
{
  ifstream f("day03.dat");

  if (!f.is_open())
    return -1;

  vector<vector<char>> land;

  while (!f.eof()) {
    string s;
    getline(f,s);

    vector<char> vc;
    vc.reserve(s.length());
    for (auto c : s)
      vc.emplace_back(c);

    land.push_back(vc);
  }

  size_t n = 0;
  size_t z = land[0].size();

  for (int row = 1, col = 3; row < land.size(); ++row, col = (col + 3) % z)
    if (land[row][col] == '#')
      n++;

  cout << n;
  return 0;
}
