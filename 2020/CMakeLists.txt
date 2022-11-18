cmake_minimum_required(VERSION 3.23)
project(2020)

set(CMAKE_CXX_STANDARD 20)

add_executable(day01a day01a.cpp)
add_executable(day01b day01b.cpp)
configure_file(day01.dat day01.dat COPYONLY)
configure_file(day01.test day01.test COPYONLY)

add_executable(day02 day02.cpp)
add_executable(day02b day02b.cpp)
configure_file(day02.dat day02.dat COPYONLY)
configure_file(day02.test day02.test COPYONLY)

add_executable(day03 day03.cpp)
configure_file(day03.dat day03.dat COPYONLY)
configure_file(day03.test day03.test COPYONLY)

add_executable(day04 day04.cpp)
add_executable(day05 day05.cpp)
add_executable(day06 day06.cpp)
add_executable(day07 day07.cpp)
add_executable(day08 day08.cpp)
add_executable(day09 day09.cpp)
add_executable(day10 day10.cpp)
