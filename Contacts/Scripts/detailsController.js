(
     function (ctx) {

         ctx.controller = function ($scope, $http, $modalInstance, id, loader) {
             var __self = this;

             $scope.data = {};
             $scope.data.titles = [
                 { id: 0, name: 'Select Title' },
                 { id: 1, name: 'Mr' },
                 { id: 2, name: 'Mrs' },
                 { id: 3, name: 'Miss' },
                 { id: 4, name: 'Ms' },
                 { id: 5, name: 'Dr' },
                 { id: 6, name: 'Prof' }
             ];
             $scope.data.genders = [
                 { id: 0, name: 'Select Gender' },
                 { id: 1, name: 'Male' },
                 { id: 2, name: 'Female' }
             ];
             $scope.data.phoneTypes = [
                 { id: 0, name: 'Select Type' },
                 { id: 1, name: 'Home' },
                 { id: 2, name: 'Work' },
                 { id: 3, name: 'Mobile' }
             ];
             $scope.data.countries = [];
             $scope.model = {
                 id: id,
                 title: $scope.data.titles[0],
                 firstName: '',
                 lastName: '',
                 gender: $scope.data.genders[0],
                 phoneType: $scope.data.phoneTypes[0],
                 phones: {
                     data: [],
                     primary: ""
                 },
                 mails: {
                     data: [],
                     primary: ""
                 },
                 addresses: {
                     data: [],
                     primary: ""
                 },
                 maleAvatarUrl: helper.actionUrl("GetAvatar", "Home", { id: -1 }),
                 femaleAvatarUrl: helper.actionUrl("GetAvatar", "Home", { id: -2 })
             };
             if ($scope.model.id) {
                 loader.getContact($scope.model.id).success(function (res) {
                     $scope.model.title = $scope.data.titles[res.Title];
                     $scope.model.firstName = res.FirstName;
                     $scope.model.lastName = res.LastName;
                     $scope.model.gender = $scope.data.titles[res.Gender];

                     for (var i = 0; i < res.Phones.length; i++) {
                         var phone = res.Phones[i];
                         $scope.model.phones.data.push({
                             phoneType: $scope.data.phoneTypes[phone.Type],
                             number: phone.Number
                         });
                         if (phone.IsPrimary)
                             $scope.model.phones.primary = phone.Number;
                     }

                     for (var j = 0; j < res.EMails.length; j++) {
                         var mail = res.EMails[j];
                         $scope.model.mails.data.push({
                             email: mail.Email
                         });
                         if (mail.IsPrimary)
                             $scope.model.mails.primary = mail.Email;
                     }

                     for (var k = 0; k < res.Addresses.length; k++) {
                         var address = res.Addresses[k];
                         $scope.model.addresses.data.push({
                             address: address.AddressVal,
                             postCode: address.PostCode,
                             country: address.Country,
                             town: address.Town,
                             isPrimary: address.IsPrimary
                         });
                         if (address.IsPrimary)
                             $scope.model.addresses.primary = address.AddressVal;
                     }
                 });
                 $scope.model.avatarUrl = helper.actionUrl("GetAvatar", "Home", { id: $scope.model.id });
             } else {
                 $scope.model.id = -1;
             }

             $scope.save = function () {
                 $http({
                     method: "POST",
                     url: helper.actionUrl("SaveOrUpdateContact", "Home"),
                     data: {
                         contact: __self.convertModelToContact()
                     }
                 }).success(function (res) {
                     if (res.Result) {
                         $modalInstance.close('updated');
                     } else {
                         alert("Error has occurred while creating / updating contact: " + res.Message);
                     }
                 });
             };

             $scope.cancel = function () {
                 $modalInstance.dismiss('cancel');
             };

             this.convertModelToContact = function () {
                 var model = $scope.model;
                 var contact = {
                     Id: model.id,
                     Title: model.title.id,
                     FirstName: model.firstName,
                     LastName: model.lastName,
                     Gender: model.gender.id,
                     Phones: [],
                     EMails: [],
                     Addresses: []
                 };
                 for (var i = 0; i < model.phones.data.length; i++) {
                     var phone = model.phones.data[i];
                     if (phone.number) {
                         contact.Phones.push({
                             Type: model.phoneType.id,
                             Number: phone.number,
                             IsPrimary: phone.number == model.phones.primary
                         });
                     }
                 }
                 for (var j = 0; j < model.mails.data.length; j++) {
                     var mail = model.mails.data[j];
                     if (mail.email) {
                         contact.EMails.push({
                             Email: mail.email,
                             IsPrimary: mail.email == model.mails.primary
                         });
                     }
                 }
                 for (var k = 0; k < model.addresses.data.length; k++) {
                     var address = model.addresses.data[k];
                     if (address.address && address.town && address.country) {
                         contact.Addresses.push({
                             AddressVal: address.address,
                             PostCode: address.postCode,
                             Country: address.country,
                             Town: address.town,
                             IsPrimary: address.address == model.addresses.primary
                         });
                     }
                 }
                 return contact;
             };

             $scope.delete = function () {
                 if (confirm("Are you sure you want to delete this contact?")) {
                     $http({
                         method: "POST",
                         url: helper.actionUrl("DeleteContact", "Home"),
                         data: {
                             id: $scope.model.id
                         }
                     }).success(function (res) {
                         if (res.Result) {
                             $modalInstance.dismiss('deleted');
                         } else {
                             alert("Error has occurred while deleting contact: " + res.Message);
                         }
                     });
                 }
             };

             $scope.addNewPhone = function () {
                 $scope.model.phones.data.push({
                     phoneType: $scope.data.phoneTypes[0],
                     number: ""
                 });
             };

             $scope.removePhone = function (phone) {
                 var indx = $scope.model.phones.data.indexOf(phone);
                 $scope.model.phones.data.splice(indx, 1);
             };

             $scope.addNewMail = function () {
                 $scope.model.mails.data.push({
                     email: ""
                 });
             };

             $scope.removeMail = function (mail) {
                 var indx = $scope.model.mails.data.indexOf(mail);
                 $scope.model.mails.data.splice(indx, 1);
             };

             $scope.addNewAddress = function () {
                 $scope.model.addresses.data.push({
                     address: "",
                     postCode: "",
                     country: "",
                     town: ""
                 });
             };

             $scope.removeAddress = function (address) {
                 var indx = $scope.model.addresses.data.indexOf(address);
                 $scope.model.addresses.data.splice(indx, 1);
             };

             $scope.getLocation = function (val) {

                 function extractCountry(item) {
                     for (var i = 0; i < item.address_components.length; i++) {
                         var component = item.address_components[i];
                         if (_.any(component.types, function (t) { return t == "country"; }))
                             return component.long_name;
                     }
                     return "";
                 }

                 function extractPostCode(item) {
                     for (var i = 0; i < item.address_components.length; i++) {
                         var component = item.address_components[i];
                         if (_.any(component.types, function (t) { return t == "postal_code"; }))
                             return component.long_name;
                     }
                     return "";
                 }

                 function extractTown(item) {
                     //Get first administrative area level
                     for (var i = 0; i < item.address_components.length; i++) {
                         var component = item.address_components[i];
                         if (_.any(component.types, function (t) { return t.indexOf("administrative_area_level") == 0; }))
                             return component.long_name;
                     }
                     return "";
                 }

                 return $http.get('http://maps.googleapis.com/maps/api/geocode/json', {
                     params: {
                         address: val,
                         sensor: false
                     }
                 }).then(function (res) {
                     var addresses = [];
                     angular.forEach(res.data.results, function (item) {
                         addresses.push(
                         {
                             address: item.formatted_address,
                             country: extractCountry(item),
                             postCode: extractPostCode(item),
                             town: extractTown(item)
                         });
                     });
                     return addresses;
                 });
             };

             $scope.addressSelected = function ($item, address) {
                 address.country = $item.country;
                 address.postCode = $item.postCode;
                 address.town = $item.town;
             };
         };
     }
)(window.details = window.details || {});