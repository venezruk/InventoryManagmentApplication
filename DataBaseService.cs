using System.Windows.Controls;
using Microsoft.Identity.Client;

namespace InventoryManagmentApplication
{
    public class DataBaseService
    {
        private static readonly ApplicationContext _dataBase = new ApplicationContext();

        public void GetWarehouseData(DataGrid dataGrid)
        {
            dataGrid.ItemsSource = _dataBase.Warehouses.ToList();
        }

        public void GetMaterialData(DataGrid dataGrid)
        {
            dataGrid.ItemsSource = _dataBase.Materials.ToList();
        }

        public void GetRouteData(DataGrid dataGrid)
        {
            dataGrid.ItemsSource = _dataBase.Routes.ToList();
        }

        public void GetSupplierData(DataGrid dataGrid)
        {
            dataGrid.ItemsSource = _dataBase.Suppliers.ToList();
        }

        public void GetProductData(DataGrid dataGrid)
        {
            dataGrid.ItemsSource = _dataBase.Products.ToList();
        }

        public void GetOrderData(DataGrid dataGrid)
        {
            dataGrid.ItemsSource = _dataBase.Orders.ToList();
        }

        public void GetTransferOrdersShipmentData(DataGrid dataGrid)
        {
            dataGrid.ItemsSource = _dataBase.TransferOrdersShipment.ToList();
        }

        public void GetShipmentDocumentData(DataGrid dataGrid)
        {
            dataGrid.ItemsSource = _dataBase.ShipmentDocuments.ToList();
        }

        public void GetTransferOrdersOnTheWayData(DataGrid dataGrid)
        {
            dataGrid.ItemsSource = _dataBase.TransferOrdersOnTheWay.ToList();
        }

        public void GetOnTheWayDocumentData(DataGrid dataGrid)
        {
            dataGrid.ItemsSource = _dataBase.OnTheWayDocuments.ToList();
        }

        public void GetTransferOrdersAcceptanceData(DataGrid dataGrid)
        {
            dataGrid.ItemsSource = _dataBase.TransferOrdersAcceptance.ToList();
        }

        public void GetAcceptanceDocumentData(DataGrid dataGrid)
        {
            dataGrid.ItemsSource = _dataBase.AcceptanceDocuments.ToList();
        }

        public void GetProcessesData(DataGrid dataGrid)
        {
            dataGrid.ItemsSource = _dataBase.Processes.ToList();
        }

        public Suppliers? GetSupplierToDelete(int selectedRowNumber) => _dataBase.Suppliers.FirstOrDefault(s => s.supplier_id == selectedRowNumber);

        public Routes? GetRouteToDelete(int selectedRowNumber) => _dataBase.Routes.FirstOrDefault(s => s.route_id == selectedRowNumber);

        public void DeleteRoute(Routes Route)
        {
            _dataBase.Routes.Remove(Route);
            _dataBase.SaveChanges();
        }

        public void DeleteSupplier(Suppliers Supplier)
        {
            _dataBase.Suppliers.Remove(Supplier);
            _dataBase.SaveChanges();
        }

        public void AddRoute(Routes Route)
        {
            _dataBase.Routes.Add(Route);
            _dataBase.SaveChanges();
        }

        public void AddSupplier(Suppliers Supplier)
        {
            _dataBase.Suppliers.Add(Supplier);
            _dataBase.SaveChanges();
        }

        public void UpdateRoutes(Routes Route)
        {
            _dataBase.Update(Route);
            _dataBase.SaveChanges();
        }

        public void UpdateSuppliers(Suppliers supplier)
        {
            _dataBase.Update(supplier);
            _dataBase.SaveChanges();
        }

        public bool IsWarehouseExist(int selectedWarehouse)
        {
            foreach (var warehouse in _dataBase.Warehouses)
            {
                if (selectedWarehouse == warehouse.warehouse_id) return true;
            }

            return false;
        }

        public void ChangeStatusAcceptance(int id)
        {
            foreach (var order in _dataBase.Orders)
            {
                if (order.order_id == id) { order.status = "На приемке"; }

                _dataBase.Update(order);
                _dataBase.SaveChanges();
            }
        }

        public Warehouses? GetWarehouseToDelete(int selectedRowNumber) => _dataBase.Warehouses.FirstOrDefault(s => s.warehouse_id == selectedRowNumber);

        public Materials? GetMaterialToDelete(int selectedRowNumber) => _dataBase.Materials.FirstOrDefault(s => s.material_id == selectedRowNumber);

        public void DeleteWarehouse(Warehouses Warehouse)
        {
            _dataBase.Warehouses.Remove(Warehouse);
            _dataBase.SaveChanges();
        }

        public void UpdateWarehouse(Warehouses Warehouse)
        {
            _dataBase.Update(Warehouse);
            _dataBase.SaveChanges();
        }

        public void UpdateMaterials(Materials Material)
        {
            _dataBase.Update(Material);
            _dataBase.SaveChanges();
        }

        public void UpdateOrders(Orders Order)
        {
            _dataBase.Update(Order);
            _dataBase.SaveChanges();
        }

        public bool IsSupplierExist(int id)
        {
            foreach (var supplier in _dataBase.Suppliers)
            {
                if (id == supplier.supplier_id) return true;
            }

            return false;
        }

        public bool IsMaterialExist(int id)
        {
            foreach (var material in _dataBase.Materials)
            {
                if (id == material.material_id) return true;
            }

            return false;
        }

        public bool IsRouteExist(int id)
        {
            foreach (var route in _dataBase.Routes)
            {
                if (id == route.route_id) return true;
            }

            return false;
        }

        public void AddWarehouse(Warehouses Warehouse)
        {
            _dataBase.Warehouses.Add(Warehouse);
            _dataBase.SaveChanges();
        }

        public void AddOrder(Orders Order)
        {
            _dataBase.Orders.Add(Order);
            _dataBase.SaveChanges();
        }

        public void AddMaterial(Materials Material)
        {
            _dataBase.Materials.Add(Material);
            _dataBase.SaveChanges();
        }

        public void DeleteMaterial(Materials Material)
        {
            _dataBase.Materials.Remove(Material);
            _dataBase.SaveChanges();
        }

        public void DeleteOrder(TransferOrdersShipment Order)
        {
            _dataBase.TransferOrdersShipment.Remove(Order);
            _dataBase.SaveChanges();
        }

        public void DeleteOrder(TransferOrdersOnTheWay Order)
        {
            _dataBase.TransferOrdersOnTheWay.Remove(Order);
            _dataBase.SaveChanges();
        }

        public void SendOrder(TransferOrdersShipment Order)
        {
            _dataBase.TransferOrdersShipment.Add(Order);
            _dataBase.SaveChanges();
        }

        public void SendOrder(TransferOrdersOnTheWay Order)
        {
            _dataBase.TransferOrdersOnTheWay.Add(Order);
            _dataBase.SaveChanges();
        }

        public void SendOrder(TransferOrdersAcceptance Order)
        {
            _dataBase.TransferOrdersAcceptance.Add(Order);
            _dataBase.SaveChanges();
        }

        public void CreateDocument(ShipmentDocuments Doc)
        {
            _dataBase.ShipmentDocuments.Add(Doc);
            _dataBase.SaveChanges();
        }

        public void CreateDocument(OnTheWayDocuments Doc)
        {
            _dataBase.OnTheWayDocuments.Add(Doc);
            _dataBase.SaveChanges();
        }

        public void CreateDocument(AcceptanceDocuments Doc)
        {
            _dataBase.AcceptanceDocuments.Add(Doc);
            _dataBase.SaveChanges();
        }

        public void ChangeStatusSending(int id)
        {
            foreach (var order in _dataBase.Orders)
            {
                if (order.order_id == id) { order.status = "В пути"; }

                _dataBase.Update(order);
                _dataBase.SaveChanges();
            }
        }

        public void ChangeStatusOrder(int id)
        {
            foreach (var order in _dataBase.Orders)
            {
                if (order.order_id == id) { order.status = "Выполнен"; }

                _dataBase.Update(order);
                _dataBase.SaveChanges();
            }
        }

        public void UpdateOrder(TransferOrdersOnTheWay Order)
        {
            _dataBase.Update(Order);
            _dataBase.SaveChanges();
        }

        public void UpdateOrder(Orders Order)
        {
            _dataBase.Update(Order);
            _dataBase.SaveChanges();
        }

        public void UpdateOrder(TransferOrdersShipment Order)
        {
            _dataBase.Update(Order);
            _dataBase.SaveChanges();
        }



        public void UpdateOrder(TransferOrdersAcceptance Order)
        {
            _dataBase.Update(Order);
            _dataBase.SaveChanges();
        }

        public Warehouses GetDestinationWarehouse(TransferOrdersAcceptance transferOrdersAcceptance)
        {
            var route_id = transferOrdersAcceptance.route_id;

            foreach (var route in _dataBase.Routes)
            {
                if (route.route_id == route_id)
                {
                    foreach (Warehouses warehouse in _dataBase.Warehouses)
                    {
                        if (warehouse.warehouse_id == route.warehouse_recipient_id) return warehouse;
                    }
                }

            }
            Warehouses warehouses = new Warehouses();

            return warehouses;
        }
    }
}
