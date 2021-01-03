using System;
using System.Collections.Generic;
using System.Text;
using UnitTestReporter.Core.Models;

namespace UnitTestReporter.Core.Extensions
{
    public static class StatusExtensions
    {
        /// <summary>
        /// Convert a string into enum Status
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Status ToStatus(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return Status.Unknown;
            }

            str = str.Trim().ToLower();

            switch (str)
            {
                case "skipped":
                case "ignored":
                case "not-run":
                case "notrun":
                case "notexecuted":
                case "not-executed":
                    return Status.Skipped;

                case "pass":
                case "passed":
                case "success":
                    return Status.Passed;

                case "warning":
                case "bad":
                case "pending":
                case "inconclusive":
                case "notrunnable":
                case "disconnected":
                case "passedbutrunaborted":
                    return Status.Inconclusive;

                case "fail":
                case "failed":
                case "failure":
                case "invalid":
                    return Status.Failed;

                case "error":
                case "aborted":
                case "timeout":
                    return Status.Error;

                default:
                    return Status.Unknown;
            }
        }

        /// <summary>
        /// Convert a Status into a string
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string ToString(this Status status)
        {
            return status.ToString().ToLower();
        }
    }
}
