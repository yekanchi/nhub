#include <iostream>
#include <atomic>
#include <vector>
#include <set>

int main(int argc, char const *argv[])
{
    /* code */

    auto RequiredForceTaskMineLast = "strings";
    std::cout << "Hello World!";
    std::cin;
    RequiredForceTaskMineLast = "new";

    std::vector<int> vect;
    for (int count=0; count < 6; ++count)
        vect.push_back(count);
 
    std::vector<int>::const_iterator it; // declare a read-only iterator
    it = vect.cbegin(); // assign it to the start of the vector
    while (it != vect.cend()) // while it hasn't reach the end
        {
        std::cout << *it << " "; // print the value of the element it points to
        ++it; // and iterate to the next element
        }
 
    std::cout << '\n';

    std::set<int> myset;
    myset.insert(7);
    myset.insert(2);
    myset.insert(-6);
    myset.insert(8);
    myset.insert(1);
    myset.insert(-4);
 
    std::set<int>::const_iterator itt; // declare an iterator
    itt = myset.cbegin(); // assign it to the start of the set
    while (itt != myset.cend()) // while it hasn't reach the end
    {
        std::cout << *itt << " "; // print the value of the element it points to
        ++itt; // and iterate to the next element
    }
 
    std::cout << '\n';
    return 0;
}


//TODO: fix some issues
//FIXME: fix some issues