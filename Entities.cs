using System.ComponentModel.DataAnnotations;

namespace InventoryManagmentApplication
{
    public class Warehouses
    {
        [Key]
        public int warehouse_id { get; set; }

        public int material_id { get; set; }

        public int current_quantity { get; set; }

        public int capacity { get; set; }

        public required string location { get; set; }

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

        public required string material_name { get; set; }

        public required string unit_of_measure { get; set; }

        public Materials() { }

        public Materials(int material_id, string material_name, string unit_of_measure)
        {
            this.material_id = material_id;
            this.material_name = material_name;
            this.unit_of_measure = unit_of_measure;
        }
    }

    public class Routes
    {
        [Key]
        public int route_id { get; set; }

        public int warehouse_sender_id { get; set; }

        public int warehouse_recipient_id { get; set; }

        public Routes() { }

        public Routes(int route_id, int warehouse_sender_id, int warehouse_recipient_id)
        {
            this.route_id = route_id;
            this.warehouse_sender_id = warehouse_sender_id;
            this.warehouse_recipient_id = warehouse_recipient_id;
        }
    }

}