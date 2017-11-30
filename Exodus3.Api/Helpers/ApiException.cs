using System;
using System.Collections;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Exodus3.Api.Helpers
{
    public class ApiException : Exception
    {
        public int StatusCode { get; set; }

        public ValidationErrorCollection Errors { get; set; }

        public ApiException(string message,
                            int statusCode = 500,
                            ValidationErrorCollection errors = null) :
            base(message)
        {
            StatusCode = statusCode;
            Errors = errors;
        }
        public ApiException(Exception ex, int statusCode = 500) : base(ex.Message)
        {
            StatusCode = statusCode;
        }
    }

    public class ApiError
    {
        public string Message { get; set; }
        public bool IsError { get; set; }
        public string Detail { get; set; }
        public ValidationErrorCollection Errors { get; set; }

        public ApiError(string message)
        {
            Message = message;
            IsError = true;
        }

        public ApiError(ModelStateDictionary modelState)
        {
            IsError = true;
            if (modelState != null && modelState.Any(m => m.Value.Errors.Count > 0))
            {
                Message = "Please correct the specified errors and try again.";
                //errors = modelState.SelectMany(m => m.Value.Errors).ToDictionary(m => m.Key, m=> m.ErrorMessage);
                //errors = modelState.SelectMany(m => m.Value.Errors.Select( me => new KeyValuePair<string,string>( m.Key,me.ErrorMessage) ));
                //errors = modelState.SelectMany(m => m.Value.Errors.Select(me => new ModelError { FieldName = m.Key, ErrorMessage = me.ErrorMessage }));
            }
        }
    }

    public class ValidationError
    {

        /// <summary>
        /// The error message for this validation error.
        /// </summary>
        public string Message { get; set;}

        /// <summary>
        /// The name of the field that this error relates to.
        /// </summary>
        public string ControlID { get; set; }

        /// <summary>
        /// An ID set for the Error. This ID can be used as a correlation between bus object and UI code.
        /// </summary>
        public string ID { get; set; }

        public ValidationError() : base() { }
        public ValidationError(string message)
        {
            Message = message;
        }
        public ValidationError(string message, string fieldName)
        {
            Message = message;
            ControlID = fieldName;
        }
        public ValidationError(string message, string fieldName, string id)
        {
            Message = message;
            ControlID = fieldName;
            ID = id;
        }

    }

    public class ValidationErrorCollection : CollectionBase
    {

        /// <summary>
        /// Indexer property for the collection that returns and sets an item
        /// </summary>
        public ValidationError this[int index]
        {
            get
            {
                return (ValidationError)List[index];
            }
            set
            {
                List[index] = value;
            }
        }

        /// <summary>
        /// Adds a new error to the collection
        /// <seealso>Class ValidationError</seealso>
        /// </summary>
        /// <param name="Error">
        /// Validation Error object
        /// </param>
        /// <returns>Void</returns>
        public void Add(ValidationError Error)
        {
            List.Add(Error);
        }


        /// <summary>
        /// Adds a new error to the collection
        /// <seealso>Class ValidationErrorCollection</seealso>
        /// </summary>
        /// <param name="Message">
        /// Message of the error
        /// </param>
        /// <param name="FieldName">
        /// optional field name that it applies to (used for Databinding errors on 
        /// controls)
        /// </param>
        /// <param name="ID">
        /// An optional ID you assign the error
        /// </param>
        /// <returns>Void</returns>
        public void Add(string Message, string FieldName = "", string ID = "")
        {
            var error = new ValidationError()
            {
                Message = Message,
                ControlID = FieldName,
                ID = ID
            };
            Add(error);
        }

        /// <summary>
        /// Like Add but allows specifying of a format  
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="FieldName"></param>
        /// <param name="ID"></param>
        /// <param name="arguments"></param>
        public void AddFormat(string Message, string FieldName, string ID, params object[] arguments)
        {
            this.Add(string.Format(Message, arguments), FieldName, ID);
        }

        /// <summary>
        /// Removes the item specified in the index from the Error collection
        /// </summary>
        /// <param name="Index"></param>
        public void Remove(int Index)
        {
            if (Index > List.Count - 1 || Index < 0)
                List.RemoveAt(Index);
        }

        /// <summary>
        /// Adds a validation error if the condition is true. Otherwise no item is 
        /// added.
        /// <seealso>Class ValidationErrorCollection</seealso>
        /// </summary>
        /// <param name="Condition">
        /// If true this error is added. Otherwise not.
        /// </param>
        /// <param name="Message">
        /// The message for this error
        /// </param>
        /// <param name="FieldName">
        /// Name of the UI field (optional) that this error relates to. Used optionally
        ///  by the databinding classes.
        /// </param>
        /// <param name="ID">
        /// An optional Error ID.
        /// </param>
        /// <returns>value of condition</returns>
        public bool Assert(bool Condition, string Message, string FieldName, string ID)
        {
            if (Condition)
                Add(Message, FieldName, ID);

            return Condition;
        }

        /// <summary>
        /// Adds a validation error if the condition is true. Otherwise no item is 
        /// added.
        /// <seealso>Class ValidationErrorCollection</seealso>
        /// </summary>
        /// <param name="Condition">
        /// If true the Validation Error is added.
        /// </param>
        /// <param name="Message">
        /// The Error Message for this error.
        /// </param>
        /// <returns>value of condition</returns>
        public bool Assert(bool Condition, string Message)
        {
            if (Condition)
                Add(Message);

            return Condition;
        }

        /// <summary>
        /// Adds a validation error if the condition is true. Otherwise no item is 
        /// added.
        /// <seealso>Class ValidationErrorCollection</seealso>
        /// </summary>
        /// <param name="Condition">
        /// If true the Validation Error is added.
        /// </param>
        /// <param name="Message">
        /// The Error Message for this error.
        /// </param>
        /// <returns>string</returns>
        public bool Assert(bool Condition, string Message, string FieldName)
        {
            if (Condition)
                Add(Message, FieldName);

            return Condition;
        }

        /// <summary>
        /// Asserts a business rule - if condition is true it's added otherwise not.
        /// <seealso>Class ValidationErrorCollection</seealso>
        /// </summary>
        /// <param name="Condition">
        /// If this condition evaluates to true the Validation Error is added
        /// </param>
        /// <param name="Error">
        /// Validation Error Object
        /// </param>
        /// <returns>value of condition</returns>
        public bool Assert(bool Condition, ValidationError Error)
        {
            if (Condition)
                List.Add(Error);

            return Condition;
        }

        /// <summary>
        /// Returns a string representation of the errors in this collection.
        /// The string is separated by CR LF after each line.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (Count < 1)
                return "";

            var sb = new StringBuilder(128);

            foreach (ValidationError Error in this)
            {
                sb.AppendLine(Error.Message);
            }

            return sb.ToString();
        }

    }
}
