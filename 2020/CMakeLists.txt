cmake_minimum_required(VERSION 3.23)
project(2020)

set(CMAKE_CXX_STANDARD 20)

add_executable(day01 day01.cpp)
configure_file(day01.dat day01.dat COPYONLY)
configure_file(day01.test day01.test COPYONLY)

