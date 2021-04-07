using System;

namespace Research
{
    /*
     * The Median3x3 filter is randomly selected for research
    */
    /*
     * Resume: 
     * the parallel algorithm is about 3 times faster than the single-threaded one on this image,
     * and LoopState is always canceled about 6 times faster than Cancelleration due to missing exceptions
    */
    /*
    * Consequences: 
    * LoopState was chosen for implementation
    */

    class Research
    {
        static void Main(string[] args)
        {
            SingleAndParallelFiltersResearch singleAndMultithreadFiltersResearch = new SingleAndParallelFiltersResearch();
            singleAndMultithreadFiltersResearch.WorkTime();

            ParallelFiltersResearch parallelFiltersResearch = new ParallelFiltersResearch();
            parallelFiltersResearch.WorkTime();

            Console.ReadLine();
        }
    }
}