using System.ComponentModel.DataAnnotations;

namespace InventoryManagmentApplication
{
    public class Warehouses
    {
        [Key]
        public int warehouse_id { get; set; }

        public int material_id { get; set; }

        public int free_quantity { get; set; }

        public int capacity { get; set; }

        public string? location { get; set; }

        public Warehouses() { }

        public Warehouses(int warehouse_id, int material_id, int free_quantity, int capacity, string location)
        {
            this.warehouse_id = warehouse_id;
            this.material_id = material_id;
            this.free_quantity = free_quantity;
            this.capacity = capacity;
            this.location = location;
        }
    }

    public class Materials
    {
        [Key]
        public int material_id { get; set; }

        public string? material_name { get; set; }

        public string? unit_of_measure { get; set; }

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

    public class Suppliers
    {
        [Key]
        public int supplier_id { get; set; }

        public string? supplier_name { get; set; }

        public string? contact_info { get; set; }

        public Suppliers() { }

        public Suppliers(int supplier_id, string supplier_name, string contact_info)
        {
            this.supplier_id = supplier_id;
            this.supplier_name = supplier_name;
            this.contact_info = contact_info;
        }
    }

    public class Products
    {
        [Key]
        public int product_id { get; set; }

        public string? product_name { get; set; }

        public Products() { }

        public Products(int product_id, string product_name)
        {
            this.product_id = product_id;
            this.product_name = product_name;
        }
    }

    public class Orders
    {
        [Key]
        public int order_id { get; set; }

        public int quantity { get; set; }

        public int material_id { get; set; }

        public int route_id { get; set; }

        public int supplier_id { get; set; }

        public required string status { get; set; }

        public Orders() { }

        public Orders(int order_id, int quantity, int material_id, int route_id , string status)
        {
            this.order_id = order_id;
            this.quantity = quantity;
            this.material_id = material_id;
            this.status = status;
        }
    }

    public class TransferOrdersShipment
    {
        [Key]
        public int shipment_id { get; set; }

        public int order_id { get; set; }

        public int quantity { get; set; }

        public int material_id { get; set; }

        public int route_id { get; set; }

        public int supplier_id { get; set; }

        public required string status { get; set; }

        public TransferOrdersShipment() { }

        public TransferOrdersShipment(int shipment_id, int order_id, int quantity, int material_id, int route_id, int supplier_id, string status)
        {
            this.shipment_id = shipment_id;
            this.order_id = order_id;
            this.quantity = quantity;
            this.material_id = material_id;
            this.route_id = route_id;
            this.supplier_id = supplier_id;
            this.status = status;
        }
    }

    public class ShipmentDocuments
    {
        [Key]
        public int document_id { get; set; }

        public int shipment_id { get; set; }

        public int order_id { get; set; }

        public int quantity { get; set; }

        public int material_id { get; set; }

        public int route_id { get; set; }

        public int supplier_id { get; set; }

        public ShipmentDocuments() { }

        public ShipmentDocuments(int document_id, int shipment_id, int order_id, int quantity, int material_id, int route_id, int supplier_id)
        {
            this.document_id = document_id;
            this.shipment_id = shipment_id;
            this.order_id = order_id;
            this.quantity = quantity;
            this.material_id = material_id;
            this.route_id = route_id;
            this.supplier_id = supplier_id;
        }
    }

    public class TransferOrdersOnTheWay
    {
        [Key]
        public int ontheway_id { get; set; }

        public int order_id { get; set; }

        public int shipment_id { get; set; }

        public int quantity { get; set; }

        public int material_id { get; set; }

        public int route_id { get; set; }

        public int supplier_id { get; set; }

        public required string status { get; set; }

        public TransferOrdersOnTheWay() { }

        public TransferOrdersOnTheWay(int ontheway_id, int order_id, int shipment_id, int quantity, int material_id, int route_id, int supplier_id, string status)
        {
            this.ontheway_id = ontheway_id;
            this.order_id = order_id;
            this.shipment_id = shipment_id;
            this.quantity = quantity;
            this.material_id = material_id;
            this.route_id = route_id;
            this.supplier_id = supplier_id;
            this.status = status;
        }
    }

    public class OnTheWayDocuments
    {
        [Key]
        public int document_id { get; set; }

        public int ontheway_id { get; set; }

        public int order_id { get; set; }

        public int shipment_id { get; set; }

        public int quantity { get; set; }

        public int material_id { get; set; }

        public int route_id { get; set; }

        public int supplier_id { get; set; }

        public OnTheWayDocuments() { }

        public OnTheWayDocuments(int document_id, int ontheway_id, int order_id, int shipment_id, int quantity, int material_id, int route_id, int supplier_id)
        {
            this.document_id = document_id;
            this.ontheway_id = ontheway_id;
            this.order_id = order_id;
            this.shipment_id = shipment_id;
            this.quantity = quantity;
            this.material_id = material_id;
            this.route_id = route_id;
            this.supplier_id = supplier_id;
        }
    }

    public class TransferOrdersAcceptance
    {
        [Key]
        public int acceptance_id { get; set; }

        public int ontheway_id { get; set; }

        public int order_id { get; set; }

        public int shipment_id { get; set; }

        public int quantity { get; set; }

        public int material_id { get; set; }

        public int route_id { get; set; }

        public int supplier_id { get; set; }

        public required string status { get; set; }

        public TransferOrdersAcceptance() { }

        public TransferOrdersAcceptance(int acceptance_id, int ontheway_id, int order_id, int shipment_id, int quantity, int material_id, int route_id, int supplier_id, string status)
        {       
            this.acceptance_id = acceptance_id;
            this.ontheway_id = ontheway_id;
            this.order_id = order_id;
            this.shipment_id = shipment_id;
            this.quantity = quantity;
            this.material_id = material_id;
            this.route_id = route_id;
            this.supplier_id = supplier_id;
            this.status = status;
        }
    }

    public class AcceptanceDocuments
    {
        [Key]
        public int document_id { get; set; }

        public int acceptance_id { get; set; }

        public int ontheway_id { get; set; }

        public int order_id { get; set; }

        public int shipment_id { get; set; }

        public int quantity { get; set; }

        public int material_id { get; set; }

        public int route_id { get; set; }

        public int supplier_id { get; set; }

        public AcceptanceDocuments() { }

        public AcceptanceDocuments(int document_id, int acceptance_id, int ontheway_id, int order_id, int shipment_id, int quantity, int material_id, int route_id, int supplier_id)
        {
            this.document_id = document_id;
            this.acceptance_id = acceptance_id;
            this.ontheway_id = ontheway_id;
            this.order_id = order_id;
            this.shipment_id = shipment_id;
            this.quantity = quantity;
            this.material_id = material_id;
            this.route_id = route_id;
            this.supplier_id = supplier_id;
        }
    }

    public class Processes
    {
        [Key]
        public int production_id { get; set; }

        public int debiting_warehouse_pyrite_id { get; set; }

        public int debiting_warehouse_oxygenium_id { get; set; }

        public int debiting_warehouse_sulfur_id { get; set; }

        public int product_warehouse_id { get; set; }

        public int product_id { get; set; }

        public required string status { get; set; }

        public Processes() { }

        public Processes(int production_id, int debiting_warehouse_pyrite_id, int debiting_warehouse_oxygenium_id, int debiting_warehouse_sulfur_id, int product_warehouse_id, int product_id, string status)
        {
            this.production_id = production_id;
            this.debiting_warehouse_pyrite_id = debiting_warehouse_pyrite_id;
            this.debiting_warehouse_oxygenium_id = debiting_warehouse_oxygenium_id;
            this.debiting_warehouse_sulfur_id = debiting_warehouse_sulfur_id;
            this.product_warehouse_id = product_warehouse_id;
            this.product_id = product_id;
            this.status = status;
        }
    }
}