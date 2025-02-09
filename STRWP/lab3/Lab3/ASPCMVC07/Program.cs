// конструктор пользователя
function Person (name, age) {
    this.name = name;
    this.age = age;
    this.sayHello = function(){
        console.log(`Person ${this.name} says "Hello"`);
    };
}
Person.prototype.print = function(){
    console.log(`Name: ${this.name}  Age: ${this.age}`);
};
 
// конструктор работника
function Employee(name, age, comp){
    Person.call(this, name, age);         // применяем конструктор Person
    this.company = comp;
    this.work = function(){
        console.log(`${this.name}  works in ${this.company}`);
    };
}
// наследуем прототип от Person
Employee.prototype = Object.create(Person.prototype);
 
// устанавливаем конструктор 
console.log(Employee.prototype.constructor);
 
// создаем объект Employee
const tom = new Employee("Tom", 39, "Google");
// обращение к унаследованному свойству
console.log("Age:", tom.age);
// обращение к унаследованному методу
tom.sayHello();    // Person Tom says "Hello"
// обращение к унаследованному методу прототипа
tom.print();    // Name: Tom  Age: 39 
// обращение к собственному методу
tom.work();    // Tom works in Google