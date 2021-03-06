﻿using System;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Sales.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.Verification.Attributes;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Sales.Commands
{
    public class ShippingAddressCommand : Command
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        [RequiredField(Group = ValidationGroup.Create | ValidationGroup.Update)]
        [StringLength(MaxLength = 30, Group = ValidationGroup.Create | ValidationGroup.Update)]
        public string Receiver { get; set; }

        [RequiredField(Group = ValidationGroup.Create | ValidationGroup.Update)]
        [StringLength(MaxLength = 20, Group = ValidationGroup.Create | ValidationGroup.Update)]
        public string ContactNo { get; set; }

        [RequiredField(Group = ValidationGroup.Create | ValidationGroup.Update)]
        [StringLength(MaxLength = 100, Group = ValidationGroup.Create | ValidationGroup.Update)]
        public string Province { get; set; }

        [RequiredField(Group = ValidationGroup.Create | ValidationGroup.Update)]
        [StringLength(MaxLength = 100, Group = ValidationGroup.Create | ValidationGroup.Update)]
        public string City { get; set; }

        [RequiredField(Group = ValidationGroup.Create | ValidationGroup.Update)]
        [StringLength(MaxLength = 100, Group = ValidationGroup.Create | ValidationGroup.Update)]
        public string Area { get; set; }

        [RequiredField(Group = ValidationGroup.Create | ValidationGroup.Update)]
        [StringLength(MaxLength = 200, Group = ValidationGroup.Create | ValidationGroup.Update)]
        public string Street { get; set; }

        [RequiredField(Group = ValidationGroup.Create | ValidationGroup.Update)]
        [StringLength(MaxLength = 200, Group = ValidationGroup.Create | ValidationGroup.Update)]
        public string Detail { get; set; }

        [RequiredField(Group = ValidationGroup.Create | ValidationGroup.Update)]
        [StringLength(MaxLength = 20, Group = ValidationGroup.Create | ValidationGroup.Update)]
        public string Postcode { get; set; }

        public bool Default { get; set; }
    }

    public class ShippingCommandHandler : CRUDTransactionCommandHandler<ShippingAddressCommand>
    {
        protected override Result Create(ShippingAddressCommand command, ITransaction transaction)
        {
            var customer = transaction.Source<Customer>().Find(e => e.Id == command.CustomerId);

            if (customer == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.InvalidCustomer));

            var shipping = new ShippingAddress
            {
                CustomerId = command.CustomerId,
                Receiver = command.Receiver,
                ContactNo = command.ContactNo,
                Province = command.Province,
                City = command.City,
                Area = command.Area,
                Street = command.Street,
                Detail = command.Detail,
                Postcode = command.Postcode
            };

            transaction.Add(shipping);

            if (command.Default)
            {
                customer.DefaultShippingId = shipping.Id;
                transaction.Update(customer);
            }

            transaction.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.CreateSuccess));
        }

        protected override Result Update(ShippingAddressCommand command, ITransaction transaction)
        {
            var customer = transaction.Source<Customer>().Find(e => e.Id == command.CustomerId);

            if (customer == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.InvalidCustomer));

            var shipping = transaction.Source<ShippingAddress>().Find(command.Id);

            if (shipping == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.RecordNotExisting));

            shipping.CustomerId = command.CustomerId;
            shipping.Receiver = command.Receiver;
            shipping.ContactNo = command.ContactNo;
            shipping.Province = command.Province;
            shipping.City = command.City;
            shipping.Area = command.Area;
            shipping.Street = command.Street;
            shipping.Detail = command.Detail;
            shipping.Postcode = command.Postcode;

            transaction.Update(shipping);

            if (command.Default && customer.DefaultShippingId != shipping.Id)
            {
                customer.DefaultShippingId = shipping.Id;
                transaction.Update(customer);
            }

            transaction.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.UpdateSuccess));
        }

        protected override Result Delete(ShippingAddressCommand command, ITransaction transaction)
        {
            var shipping = transaction.Source<ShippingAddress>().Find(command.Id);

            if (shipping == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.RecordNotExisting));

            transaction.Remove(shipping);
            transaction.Commit();

            return Result.Success(message: Localization.Get(Resources.Key.Command.DeleteSuccess));
        }
    }
}
