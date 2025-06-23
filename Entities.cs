using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentApplication
{
    public class Warehouses
    {
        [Key]
        public int warehouse_id {  get; set; }

        public int material_id { get; set; }

        public int current_quantity { get; set; }

        public int capacity { get; set; }

        private string _location;

        public required string location
        {
            get => _location;
            set => _location = value;
        }

        public Warehouses() { }

        public Warehouses(int warehouse_id, int material_id, int current_quantity, int capacity, string location)
        {
            this.warehouse_id = warehouse_id;
            this.material_id = material_id;
            this.current_quantity = current_quantity;
            this.capacity = capacity;
            this.location = location;
        }
    }

    public class Materials
    {
        [Key]
        public int material_id { get; set; }

        private string _material_name;
        private string _unit_of_measure;
        private string _min_stock_level;

        public required string material_name
        {
            get => _material_name;
            set => _material_name = value;
        }

        public required string unit_of_measure
        {
            get => _unit_of_measure;
            set => _unit_of_measure = value;
        }

        public required string min_stock_level
        {
            get => _min_stock_level;
            set => _min_stock_level = value;
        }

        public Materials() { }

        public Materials(int material_id, string _material_name, string unit_of_measure, string min_stock_level)
        {
            this.material_id = material_id;
            this._material_name = _material_name;
            this.unit_of_measure = unit_of_measure;
            this.min_stock_level = min_stock_level;
        }
    }
}