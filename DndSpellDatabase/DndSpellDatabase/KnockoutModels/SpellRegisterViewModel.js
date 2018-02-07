var spellRegisterViewModel;

function Spell(id, name, range, damageEffect, description, level) {
    var self = this;

    self.Id = ko.observable(id);
    self.Name = ko.observable(name);
    self.Range = ko.observable(range);
    self.DamageEffect = ko.observable(damageEffect);
    self.Description = ko.observable(description);
    self.Level = ko.observable(level);

    self.levels = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"];

    self.addSpell = function () {
        var dataObject = ko.toJSON(this);
        $.ajax({
            url: '/api/Spell',
            type: 'post',
            data: dataObject,
            contentType: 'application/json',
            success: function (data) {
                spellRegisterViewModel.spellListViewModel.spells.push(new Spell(data.Id, data.Name, data.Range, data.DamageEffect, data.Description, data.Level));

                self.Id(null);
                self.Name('');
                self.Range('');
                self.DamageEffect('');
                self.Description('');
            }
        });
    };
}

function SpellList() {

    var self = this;

    self.spells = ko.observableArray([]);

    self.getSpells = function () {
        self.spells.removeAll();

        $.getJSON('/api/Spell', function (data) {
            $.each(data, function (key, value) {
                self.spells.push(new Spell(value.Id, value.Name, value.Range, value.DamageEffect, value.Description, value.Level));
            });
        });
    };

    self.removeSpell = function (spell) {
        $.ajax({
            url: '/api/spell/' + spell.Id(),
            type: 'delete',
            contentType: 'application/json',
            success: function () {
                self.spells.remove(spell);
            }
        });
    };
}

spellRegisterViewModel = { addSpellViewModel: new Spell(), spellListViewModel: new SpellList() };

$(document).ready(function () {

    ko.applyBindings(spellRegisterViewModel);

    spellRegisterViewModel.spellListViewModel.getSpells();
});