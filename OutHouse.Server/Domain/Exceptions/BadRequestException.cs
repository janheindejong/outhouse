﻿namespace OutHouse.Server.Domain.Exceptions
{
    public class BadRequestException(string message) : OuthouseException(message)
    {
    }
}
