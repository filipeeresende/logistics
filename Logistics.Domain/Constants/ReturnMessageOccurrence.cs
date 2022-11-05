using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.Domain.Constants
{
    public static class ReturnMessageOccurrence
    {
        public const string MessageOccurenceNotFound = "Occurrence not found.";
        public const string MessageOccurencesNotFound = "Occurrences not found.";
        public const string MessageInsertOcorrence = "Occurrence registered successfully.";
        public const string MessageOccurenceType = "Could not record this event.";
        public const string MessageOccurrenceStatus = "Occurrence cannot be excluded.";
        public const string MessageDeleteOccurrence = "Occurrence successfully deleted";
    }
}
