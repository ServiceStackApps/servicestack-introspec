﻿// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this 
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

namespace ServiceStack.IntroSpec.Validators
{
    using FluentValidation;
    using Settings;

    public class ApiSpecConfigValidator : AbstractValidator<ApiSpecConfig>
    {
        public ApiSpecConfigValidator()
        {
            RuleFor(c => c.Contact).NotNull().SetValidator(new ApiContactValidator());
            RuleFor(c => c.Description).NotEmpty();
        }
    }
}