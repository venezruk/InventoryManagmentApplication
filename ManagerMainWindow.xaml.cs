using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace InventoryManagmentApplication
{
    /// <summary>
    /// Логика взаимодействия для ManagerMainWindow.xaml
    /// </summary>
    public partial class ManagerMainWindow : Window
    {
        DataBaseService dataBaseService = new DataBaseService();

        public ManagerMainWindow()
        {
            InitializeComponent();

            // Загрузка данных из базы данных в таблицы //

            dataBaseService.GetWarehouseData(WarehousesTable);

            dataBaseService.GetMaterialData(MaterialsTable);

            dataBaseService.GetRouteData(RoutesTable);

            dataBaseService.GetSupplierData(SuppliersTable);

            dataBaseService.GetProductData(ProductsTable);

            dataBaseService.GetOrderData(OrdersTable);

            dataBaseService.GetTransferOrdersShipmentData(OrdersShipmentTable);

            dataBaseService.GetShipmentDocumentData(ShipmentDocumentsTable);

            dataBaseService.GetTransferOrdersOnTheWayData(OrdersOnTheWayTable);

            dataBaseService.GetOnTheWayDocumentData(OnTheWayDocumentsTable);

            dataBaseService.GetTransferOrdersAcceptanceData(OrdersAcceptanceTable);

            dataBaseService.GetAcceptanceDocumentData(AcceptanceDocumentsTable);

            // //

            // Корректная обработка захвата и фокуса для интерактивной работы с таблицами //

            MaterialsTable.AddHandler(System.Windows.Controls.DataGrid.KeyDownEvent,
            new KeyEventHandler(MaterialsTableKeyDown),
            handledEventsToo: true);

            OrdersTable.AddHandler(System.Windows.Controls.DataGrid.KeyDownEvent,
            new KeyEventHandler(OrdersTableKeyDown),
            handledEventsToo: true);

            OrdersAcceptanceTable.AddHandler(System.Windows.Controls.DataGrid.KeyDownEvent,
            new KeyEventHandler(OrdersAcceptanceTableKeyDown),
            handledEventsToo: true);

            ProductsTable.AddHandler(System.Windows.Controls.DataGrid.KeyDownEvent,
                new KeyEventHandler(ProductsTableKeyDown), handledEventsToo: true);

            // //
        }

        private void AddingMaterialDataClick(object sender, RoutedEventArgs e)
        {
            var newMaterial = new Materials()
            {
                material_name = "Название",
                unit_of_measure = "л"
            };

            dataBaseService.AddMaterial(newMaterial);

            dataBaseService.GetMaterialData(MaterialsTable);

            MaterialsTable.Items.Refresh();
        }

        private void MaterialsTableCellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var material = MaterialsTable.SelectedItem as Materials;

            if (material != null)
            {
                dataBaseService.UpdateMaterials(material);
            }
        }

        private void MaterialsTableKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {

                var answer = MessageBox.Show("Вы действительно хотите удалить информацию о материале?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (answer == MessageBoxResult.Yes)
                {

                    var selectedMaterial = (Materials)MaterialsTable.SelectedItem;

                    if (selectedMaterial != null)
                    {
                        int selectedRowNumber = selectedMaterial.material_id;

                        var materialToDelete = dataBaseService.GetMaterialToDelete(selectedRowNumber);

                        if (materialToDelete != null)
                        {
                            dataBaseService.DeleteMaterial(materialToDelete);

                            MessageBox.Show("Информация успешно удалена!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);

                        }
                        dataBaseService.GetMaterialData(MaterialsTable);
                    }
                }
                else
                {
                    MessageBox.Show("Данные не были удалены", "Отмена очистки", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void AddingOrderDataClick(object sender, EventArgs e)
        {
            var newOrder = new Orders()
            {
                status = "В обработке",
                supplier_id = 1,
                material_id = 1,
                quantity = 1000,
                route_id = 7,
            };

            dataBaseService.AddOrder(newOrder);

            dataBaseService.GetOrderData(OrdersTable);

            OrdersTable.Items.Refresh();
        }

        private void OrdersTableCellEditEnding(object sender, EventArgs e)
        {
            var order = OrdersTable.SelectedItem as Orders;

            if (order != null)
            {
                if (!dataBaseService.IsMaterialExist(order.material_id)) MessageBox.Show("Выбран id несуществующего материала.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);

                else
                {
                    if (!dataBaseService.IsRouteExist(order.route_id)) MessageBox.Show("Выбран id несуществующего маршрута.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);

                    else
                    {
                        if (!dataBaseService.IsSupplierExist(order.supplier_id)) MessageBox.Show("Выбран id несуществующего поставщика.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                        else
                        {
                            dataBaseService.UpdateOrders(order);
                        }
                    }
                }
            }
        }

        private void OrdersTableKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var selectedOrder = (Orders)OrdersTable.SelectedItem;

                if (selectedOrder.status != "В обработке") MessageBox.Show($"Данный заказ уже был обработан (направлен на отгрузку).", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                else
                {
                    var answer = MessageBox.Show($"Вы действительно хотите отгрузить заказ №{selectedOrder.order_id}?", "Подтверждение отгрузки", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (answer == MessageBoxResult.Yes)
                    {
                        if (selectedOrder != null)
                        {
                            var newShipmentOrder = new TransferOrdersShipment
                            {
                                status = "Выполняется",
                                material_id = selectedOrder.material_id,
                                order_id = selectedOrder.order_id,
                                quantity = selectedOrder.quantity,
                                route_id = selectedOrder.route_id,
                                supplier_id = selectedOrder.supplier_id,
                            };

                            dataBaseService.SendOrder(newShipmentOrder);

                            selectedOrder.status = "Обработан";

                            dataBaseService.UpdateOrder(selectedOrder);

                            var newDocument = new ShipmentDocuments
                            {
                                material_id = newShipmentOrder.material_id,
                                order_id = newShipmentOrder.order_id,
                                quantity = newShipmentOrder.quantity,
                                route_id = newShipmentOrder.route_id,
                                shipment_id = newShipmentOrder.shipment_id,
                                supplier_id = newShipmentOrder.supplier_id,
                            };

                            dataBaseService.CreateDocument(newDocument);

                            MessageBox.Show($"Заказ №{newShipmentOrder.order_id} был обработан.", "Обработка заказа", MessageBoxButton.OK, MessageBoxImage.Information);

                            dataBaseService.GetOrderData(OrdersTable);

                            dataBaseService.GetTransferOrdersShipmentData(OrdersShipmentTable);

                            dataBaseService.GetShipmentDocumentData(ShipmentDocumentsTable);

                            dataBaseService.GetTransferOrdersOnTheWayData(OrdersOnTheWayTable);
                        }
                    }
                    else MessageBox.Show($"Заказ №{selectedOrder.order_id} не был отправлен.", "Отмена действия", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void OrdersAcceptanceTableKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var selectedOrder = (TransferOrdersAcceptance)OrdersAcceptanceTable.SelectedItem;

                if (selectedOrder.status == "Выполнен") MessageBox.Show($"Данный заказ уже был завершен.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                else
                {
                    var answer = MessageBox.Show($"Вы действительно хотите завершить заказ №{selectedOrder.order_id}?", "Подтверждение завершения", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (answer == MessageBoxResult.Yes)
                    {
                        if (selectedOrder != null)
                        {
                            var warehouse = dataBaseService.GetDestinationWarehouse(selectedOrder);

                            if (selectedOrder.quantity > warehouse.free_quantity) MessageBox.Show($"Не удалось завершить заказ, поскольку количество запасов на приемке ({selectedOrder.quantity}) больше свободного места на складе ({warehouse.free_quantity})", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);

                            else
                            {
                                warehouse.free_quantity = warehouse.free_quantity - selectedOrder.quantity;

                                dataBaseService.UpdateWarehouse(warehouse);

                                selectedOrder.status = "Выполнен";

                                dataBaseService.UpdateOrder(selectedOrder);

                                var newDocument = new AcceptanceDocuments
                                {
                                    acceptance_id = selectedOrder.acceptance_id,
                                    material_id = selectedOrder.material_id,
                                    order_id = selectedOrder.order_id,
                                    quantity = selectedOrder.quantity,
                                    route_id = selectedOrder.route_id,
                                    shipment_id = selectedOrder.shipment_id,
                                    supplier_id = selectedOrder.supplier_id,
                                    ontheway_id = selectedOrder.ontheway_id
                                };

                                dataBaseService.CreateDocument(newDocument);

                                MessageBox.Show($"Заказ №{selectedOrder.order_id} был завершен.", "Завершение заказа", MessageBoxButton.OK, MessageBoxImage.Information);

                                dataBaseService.GetOrderData(OrdersTable);

                                dataBaseService.ChangeStatusOrder(selectedOrder.order_id);

                                dataBaseService.GetAcceptanceDocumentData(AcceptanceDocumentsTable);

                                dataBaseService.GetTransferOrdersAcceptanceData(OrdersAcceptanceTable);
                            }
                        }
                    }
                    else MessageBox.Show($"Заказ №{selectedOrder.order_id} не был завершен.", "Отмена действия", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void AddingProcessDataClick(object sender, RoutedEventArgs e)
        {
            var newProcess = new Processes()
            {
                status = "В обработке",
                debiting_warehouse_sulfur_id = 5,
                debiting_warehouse_oxygenium_id = 4,
                debiting_warehouse_pyrite_id = 1,
                product_warehouse_id = 6
            };

            dataBaseService.AddProcess(newProcess);

            dataBaseService.GetProcessesData(ProcessesTable);

            ProcessesTable.Items.Refresh();

        }

        private void AddingProductDataClick(object sender, RoutedEventArgs e)
        {
            var newProduct = new Products()
            {
                product_name = "Новый продукт"
            };


            dataBaseService.AddProduct(newProduct);

            dataBaseService.GetProductData(ProductsTable);

            ProductsTable.Items.Refresh();
        }
        private void ProductsTableCellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var product = ProductsTable.SelectedItem as Products;

            if (product != null)
            {
                dataBaseService.UpdateProducts(product);
            }
        }

        private void ProductsTableKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {

                var answer = MessageBox.Show("Вы действительно хотите удалить информацию о продукте?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (answer == MessageBoxResult.Yes)
                {

                    var selectedProduct = (Products)ProductsTable.SelectedItem;

                    if (selectedProduct != null)
                    {
                        int selectedRowNumber = selectedProduct.product_id;

                        var productToDelete = dataBaseService.GetProductToDelete(selectedRowNumber);

                        if (productToDelete != null)
                        {
                            dataBaseService.DeleteProduct(productToDelete);

                            MessageBox.Show("Информация успешно удалена!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);

                        }
                        dataBaseService.GetProductData(ProductsTable);
                    }
                }
                else
                {
                    MessageBox.Show("Данные не были удалены", "Отмена очистки", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}