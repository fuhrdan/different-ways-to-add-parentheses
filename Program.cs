//*****************************************************************************
//** 241. Different Ways to Add Parentheses   leetcode                       **
//*****************************************************************************


/**
 * Note: The returned array must be malloced, assume caller calls free().
 */
// Function to calculate the result based on operator
int calc(int val1, int val2, char op) {
    if (op == '+') return val1 + val2;
    else if (op == '-') return val1 - val2;
    else if (op == '*') return val1 * val2;
    return INT_MIN;  // or handle invalid operator
}

// Helper function to convert a substring of a string to an integer
int strToInt(char* str, int start, int end) {
    char temp[end - start + 2];  // +1 for '\0'
    strncpy(temp, str + start, end - start + 1);
    temp[end - start + 1] = '\0';
    return atoi(temp);
}

// Recursive function to compute all possible results
int* diffWaysToCompute(char* input, int* returnSize) {
    int len = strlen(input);
    if (len == 0) {
        *returnSize = 0;
        return NULL;
    }

    // Check if the entire input is a number
    int i = 0;
    while (i < len && isdigit(input[i])) i++;
    if (i == len) {
        int* result = (int*)malloc(sizeof(int));
        result[0] = atoi(input);
        *returnSize = 1;
        return result;
    }

    // If input contains operators, split and recursively compute results
    int* res = NULL;
    *returnSize = 0;

    i = 0;
    while (i < len) {
        if (!isdigit(input[i])) {  // Found an operator

            // Left part (substring before the operator)
            int leftSize;
            int* left = diffWaysToCompute(strndup(input, i), &leftSize);

            // Right part (substring after the operator)
            int rightSize;
            int* right = diffWaysToCompute(strdup(input + i + 1), &rightSize);

            // Combine left and right parts
            for (int l = 0; l < leftSize; l++) {
                for (int r = 0; r < rightSize; r++) {
                    int value = calc(left[l], right[r], input[i]);
                    res = (int*)realloc(res, (*returnSize + 1) * sizeof(int));
                    res[*returnSize] = value;
                    (*returnSize)++;
                }
            }

            free(left);
            free(right);
        }
        i++;
    }

    return res;
}