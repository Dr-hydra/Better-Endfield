#pragma once
#include <cstdlib>
#include <iostream>
#include <cmath>
inline unsigned checks = 0;
#define CHECK(x) do { ++checks; if (!(x)) { std::cerr << __FILE__ << ':' << __LINE__ << ": " << #x << '\n'; std::exit(1); } } while (false)
inline bool near(double a,double b,double epsilon=1e-5) { return std::abs(a-b)<epsilon; }
