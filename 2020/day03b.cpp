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

  size_t z = land[0].size();

  int rg[] = {1, 3, 5, 7, 1};
  int dw[] = {1, 1, 1, 1, 2};
  size_t res[] = {0,0,0,0, 0};

  for (int i=0;i<5;++i) {
    for (size_t row = dw[i], col = rg[i]; row < land.size(); row += dw[i], col = (col + rg[i]) % z)
      if (land[row][col] == '#')
        res[i]++;
  }

  cout << res[0] << " " << res[1] << " " << res[2] << " " << res[3] << " " << res[4] << " " << res[0] * res[1] * res[2] * res[3] * res[4];
  return 0;
}
